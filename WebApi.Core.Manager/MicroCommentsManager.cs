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
    public class MicroCommentsManager: IMicroCommentsManager
    {
        private readonly IMicroCommentsService _microComments;

        public MicroCommentsManager(IMicroCommentsService microComments)
        {
            _microComments = microComments ??
                throw new ArgumentNullException(nameof(microComments));
        }
        public async Task<IEnumerable<MicroCommentsDto>> GetMicroCommentsForMicroBlog(Guid microId)
        {
            if (microId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(microId));
            }

            return await _microComments.GetAllAsync()
                .Where(x => x.MicroBlogId == microId)
                .Select(m => new MicroCommentsDto()
            {
                CommentsContent = m.CommentsContent,
                UserName = m.User.UserName,
                CreateTime = m.CreateTime,
                UserId = m.UserId,
                MicroBlogId = m.MicroBlogId
            }).OrderByDescending(m=>m.CreateTime)
                .ToListAsync();
        }

        public async Task CreateMicroComment(Guid userId, Guid microId, MicroComments microComments)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (microId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(microId));
            }
            microComments.UserId = userId;
            microComments.MicroBlogId = microId;
            await _microComments.CreateAsync(microComments);
        }

        public async Task DeleteMicroCommentForUser(Guid commentId)
        {
            if (commentId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(commentId));
            }
            await _microComments.RemoveAsync(commentId);
        }

        public async Task<bool> ExistsMicroComment(Guid commentId)
        {
            if (commentId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(commentId));
            }

            return await _microComments.GetAllAsync().AnyAsync(x => x.Id == commentId);

        }
    }
}