using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.IManager
{
    public interface IMicroCommentsManager
    {
        Task<IEnumerable<MicroCommentsDto>> GetMicroCommentsForMicroBlog(Guid microId);

        Task CreateMicroComment(Guid userId, Guid microId, MicroComments microComments);

        Task DeleteMicroCommentForUser(Guid commentId);

        Task<bool> ExistsMicroComment(Guid commentId);
    }
}