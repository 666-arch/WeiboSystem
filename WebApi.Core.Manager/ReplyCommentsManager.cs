using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Dto;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.Manager
{
    public class ReplyCommentsManager:IReplyCommentsManager
    {
        private readonly IReplyCommentsService _reply;

        public ReplyCommentsManager(IReplyCommentsService reply)
        {
            _reply = reply ?? 
                     throw new ArgumentNullException(nameof(reply));
        }
        public async Task CreateReplyComment
            (Guid userId, Guid commentId, Guid targetCommentId, Guid targetUserId, ReplyComments replyComments)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            replyComments.RespondUserId = userId;
            replyComments.MicroCommentId = commentId;
            replyComments.ReplyToTargetCommentId = targetCommentId;
            replyComments.RespondTargetUserId = targetUserId;
            await _reply.CreateAsync(replyComments);
        }
        
        public async Task<IEnumerable<ReplyCommentsDto>> GetAllReplyComment(Guid commentId)
        {
            return await _reply.GetAllAsync()
                .Where(x => x.MicroComments.Id == commentId)
                .Select(m => new ReplyCommentsDto()
            {
                    UserName = m.User.UserName,  //回复的用户名
                    ImagePath = m.User.ImagePath,   //回复人头像
                    ByReplyNickName=m.Users.UserName,   //被回复用户
                    MicroCommentId=m.MicroCommentId,    //回复评论Id
                    ReplyToTargetCommentId=m.ReplyToTargetCommentId,    //回复目标评论Id
                    ReplyContent=m.ReplyContent,    //回复内容
                    RespondUserId=m.RespondUserId,  //回复用户Id(针对评论的回复)
                    RespondTargetUserId=m.RespondTargetUserId,  //回复目标用户Id(针对回复的回复)
                    CreateTime = m.CreateTime   //回复时间
                }).OrderByDescending(x=>x.CreateTime)
                .ToListAsync();
        }
    }
}