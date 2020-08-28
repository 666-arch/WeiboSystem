using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.IManager
{
    public interface IReplyCommentsManager
    {
        Task CreateReplyComment(Guid userId, Guid commentId, Guid targetCommentId, Guid targetUserId,
            ReplyComments replyComments);

        Task<IEnumerable<ReplyCommentsDto>> GetAllReplyComment(Guid commentId);

    }
}