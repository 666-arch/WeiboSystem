
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.Models;
using WebApi.Core.RunApi.Helpers;

namespace WebApi.Core.RunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MicroBlogController:ControllerBase
    {
        private readonly IUserManager _user;
        private readonly IMicroBlogManger _micro;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;
        private readonly IHttpContextAccessor _accessor;
        private readonly IFormCollection _formCollection;

        private readonly IDatabase _db;
        private readonly string keyCounts = "LikeCounts";

        public MicroBlogController(IUserManager user,
            IMicroBlogManger micro,
            IMapper mapper, 
            IConnectionMultiplexer redis,
            IHttpContextAccessor accessor
            )
        {
            _user = user ?? 
                    throw new ArgumentNullException(nameof(user));
            _micro = micro ?? 
                     throw new ArgumentNullException(nameof(micro));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));

            _redis = redis;
            _accessor = accessor;
            _db = _redis.GetDatabase(6);

        }

        [HttpGet("{userId}",Name = nameof(GetMicroBlog))]
        public async Task<ActionResult<IEnumerable<MicroBlogDto>>> 
            GetMicroBlog(Guid userId)   //已完成
        {
            if (!await _user.UserExistsAsync(userId))
            {
                return NotFound();
            }
            var microBlogEntity =await _micro.GetMicroBlogForUser(userId);

            if (microBlogEntity==null)
            {
                return NotFound();
            }
            var microBlogDto= _mapper.Map<IEnumerable<MicroBlogDto>>(microBlogEntity);  //映射回 Dto
            return Ok(microBlogDto);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<MicroBlogDto>> 
            CreateMicro(Guid userId,MicroBlogAddDto micro)    //已完成
        {
            if (!await _user.UserExistsAsync(userId))
            {
                return NotFound();
            }
            var microEntity = _mapper.Map<MicroBlog>(micro);    //映射到 entity
            await _micro.CreateMicroBlog(userId,microEntity);
            var dtoToReturn= _mapper.Map<MicroBlogDto>(microEntity);    //映射回 dto
            return CreatedAtRoute(
               nameof(GetMicroBlog),
               new {userId = dtoToReturn.UserId},dtoToReturn);
        }

        [HttpGet(Name = nameof(GetMicroBlogList))]
        public async Task<ActionResult<IEnumerable<MicroBlogDto>>>
            GetMicroBlogList([FromQuery]MicroBlogDtoParameters microBlogDtoParameters)  //已完成
        {
            if (microBlogDtoParameters==null)
            {
                throw new ArgumentNullException(nameof(microBlogDtoParameters));
            }
            var micros = await _micro.GetMicroBlogs(microBlogDtoParameters);

            //创建前一页 link
            var previousLink = micros.HasPrevious
                ? CreateMicroBlogResourceUri(microBlogDtoParameters, ResourceUriType.PreviousPage)
                : null;

            //创建下一页 link
            var nextLink = micros.HasNext
                ? CreateMicroBlogResourceUri(microBlogDtoParameters, ResourceUriType.NextPage)
                : null;

            //元数据
            var paginationMetadata = new
            {
                totalCount = micros.TotalCount,
                pageSize = micros.PageSize,
                current = micros.CurrentPage,
                totalPages = micros.TotalPages,
                previousLink,
                nextLink
            };
            //添加自定义的 headers，供 api 消费者调用
            Response.Headers.Add("S-PaginationSource",
                JsonSerializer.Serialize(paginationMetadata,new JsonSerializerOptions
                {
                    // 避免转义符
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
            var microBlogDto = _mapper.Map<IEnumerable<MicroBlogDto>>(micros);
            return Ok(microBlogDto);
        }

        //https://localhost:5001/api/MicroBlog/DeleteMicroForUser/userId/microId
        [HttpDelete("{userId}/{microId}")]
        public async Task<IActionResult> 
            DeleteMicroForUser(Guid userId,Guid microId)
        {
            if (!await _user.UserExistsAsync(userId))
            {
                return NotFound();
            }
            var entity =await _micro.GetMicroForUser(userId, microId);
            if (entity==null)
            {
                return NotFound();
            }
            await _micro.RemoveMicroBlog(microId);
            return NoContent();
        }

        private string CreateMicroBlogResourceUri
            (MicroBlogDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                //前一页
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetMicroBlogList), new
                    {
                        pageNumber=parameters.PageNumber-1,
                        pageSize=parameters.PageSize,
                        microContent=parameters.MicroContent,
                        searchQuery=parameters.SearchQuery
                    });
                //后一页
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetMicroBlogList), new
                    {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        microContent = parameters.MicroContent,
                        searchQuery = parameters.SearchQuery
                    });
                //当前页
                default:return Url.Link(nameof(GetMicroBlogList), new
                    {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        microContent = parameters.MicroContent,
                        searchQuery = parameters.SearchQuery
                    });
            }
        }
        [HttpGet("{microId}")]
        public async Task<IActionResult>
            StatisticalLikeCount(Guid microId,int count=0)  //单独定义点赞接口
        {
            if (!await _micro.ExistsMicroBlog(microId))
            {
                return NotFound();
            }

            if (await _db.
                KeyExistsAsync
                    (keyCounts+_accessor.HttpContext.Connection.RemoteIpAddress.ToString()+count)==false)
            {
                await _db.StringIncrementAsync(keyCounts + count, 1);

                await _db.StringSetAsync(
                    keyCounts + _accessor.HttpContext.Connection.RemoteIpAddress.ToString() + count,"true");
               
                string totals = await _db.StringGetAsync(keyCounts + count);
                TotalLikeCountMicroBlogModel likes = new TotalLikeCountMicroBlogModel
                {
                    LikeCount = Convert.ToInt32(totals)
                };
                await _micro.EditMicroBlogLikeCounts(microId, likes.LikeCount);
                return Ok(likes);
            }
            //读取
            string total = await _db.StringGetAsync(keyCounts + count);
            TotalLikeCountMicroBlogModel like=new TotalLikeCountMicroBlogModel
            {
                LikeCount = Convert.ToInt32(total)

            };
            await _micro.EditMicroBlogLikeCounts(microId, like.LikeCount);
            return Ok(like);
        }

        [HttpPost]
        public async Task<string> 
            PostFile([FromForm]IFormCollection collection)
        {
            string result = "Fail";
            if (collection.ContainsKey("path"))
            {
                var path = collection["path"];
            }

            FormFileCollection fileCollection = (FormFileCollection) collection.Files;
            foreach (IFormFile file in fileCollection)
            {
                StreamReader reader=new StreamReader(file.OpenReadStream());
                string content = await reader.ReadToEndAsync();
                string name = file.FileName;
                string filename = @"F:/File/" + name;
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                await using (FileStream fs= System.IO.File.Create(filename))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                result = "成功!";
            }
            return result;
        }
    }
}