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
    public class ReplyCommentsController:ControllerBase
    {
        private readonly IReplyCommentsManager _reply;
        private readonly IMicroCommentsManager _comment;
        private readonly IMicroBlogManger _micro;
        private readonly IMapper _mapper;
        
        public ReplyCommentsController(IReplyCommentsManager reply,
            IMicroCommentsManager comment,
            IMicroBlogManger micro,
            IMapper mapper)
        {
            _reply = reply ?? 
                     throw new ArgumentNullException(nameof(reply));
            _comment = comment ?? 
                       throw new ArgumentNullException(nameof(comment));
            _micro = micro ?? 
                     throw new ArgumentNullException(nameof(micro));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{commentId}", Name = nameof(GetReplyComments))]
        public async Task<ActionResult<IEnumerator<ReplyCommentsDto>>>
            GetReplyComments(Guid commentId)
        {
            if (!await _comment.ExistsMicroComment(commentId))
            {
                return NotFound();
            }
            var replyEntity = await _reply.GetAllReplyComment(commentId);
            if (replyEntity == null)
            {
                return NotFound();
            }
            return Ok(replyEntity);
        }

        [HttpPost("{userId}/{commentId}/{targetCommentId}/{targetUserId}")]
        public async Task<ActionResult<IEnumerable<ReplyCommentsDto>>> 
            CreateReplyComments(Guid userId,Guid commentId, Guid targetCommentId,Guid targetUserId, ReplyCommentsAddDto replyComments)
        {
            var replyEntity= _mapper.Map<ReplyComments>(replyComments);
            await _reply.CreateReplyComment(userId, commentId, targetCommentId, targetUserId, replyEntity);
            var dtoToReturn = _mapper.Map<ReplyCommentsDto>(replyEntity);
            return CreatedAtRoute(nameof(GetReplyComments), new {commentId = dtoToReturn.MicroCommentId},dtoToReturn);
        }
    }
}