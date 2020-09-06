using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MicroCommentsController:ControllerBase
    {
        private readonly IMicroCommentsManager _microComments;
        private readonly IMicroBlogManger _microBlog;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public MicroCommentsController(
            IMicroCommentsManager microComments,
            IMicroBlogManger microBlog,
            IMapper mapper,
            IConnectionMultiplexer redis)
        {
            _microComments = microComments ?? 
                             throw new ArgumentNullException(nameof(microComments));
            _microBlog = microBlog ??
                         throw new ArgumentNullException(nameof(microBlog)); 
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
            _redis = redis;
            _db = _redis.GetDatabase();
        }
        [HttpGet("{microId}",
            Name = nameof(GetComments))]
        public async Task<ActionResult<IEnumerable<MicroCommentsDto>>> 
            GetComments(Guid microId)
        {
            if (!await _microBlog.ExistsMicroBlog(microId))
            {
                return NotFound();
            }
            var CommentEntity= await _microComments.GetMicroCommentsForMicroBlog(microId);

            if (CommentEntity==null)
            {
                return NotFound();
            }
            var commentDto= _mapper.Map<IEnumerable<MicroCommentsDto>>(CommentEntity);
            return Ok(commentDto);
        }

        [HttpPost("{userId}/{microId}")]
        public async Task<ActionResult<IEnumerable<MicroCommentsDto>>>
            CreateMicroCommentForMicroBlog(Guid userId,Guid microId, MicroCommentsAddDto microComments)
        {
            var CommentEntity = _mapper.Map<MicroComments>(microComments);  //映射到 entity 
            await _microComments.CreateMicroComment(userId, microId, CommentEntity);
            var dtoToReturns= _mapper.Map<MicroCommentsDto>(CommentEntity);
            //返回 201 提供 uir 给api消费者
            return CreatedAtRoute(nameof(GetComments),
                new {microId = dtoToReturns.MicroBlogId}, 
                dtoToReturns);
        }

        [HttpDelete("{microId}/{commentId}")]
        public async Task<IActionResult>
            RemoveComment(Guid microId,Guid commentId)
        {
            if (!await _microBlog.ExistsMicroBlog(microId))
            {
                return NotFound();
            }
            await _microComments.DeleteMicroCommentForUser(commentId);
            return NoContent();
        }

    }
}