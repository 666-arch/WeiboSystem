using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public MicroBlogController(IUserManager user,IMicroBlogManger micro,IMapper mapper)
        {
            _user = user ?? 
                    throw new ArgumentNullException(nameof(user));
            _micro = micro ?? 
                     throw new ArgumentNullException(nameof(micro));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
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
    }
}