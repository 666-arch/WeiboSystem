using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Dto;
using WebApi.Core.IManager.Helper;
using WebApi.Core.Models;

namespace WebApi.Core.IManager
{
    public interface IMicroBlogManger
    {
        Task CreateMicroBlog(Guid userId, MicroBlog micro);
        Task<PageList<MicroBlogDto>> GetMicroBlogs(MicroBlogDtoParameters microBlogDtoParameters);
        Task<IEnumerable<MicroBlogDto>> GetMicroBlogForUser(Guid userId);
        Task RemoveMicroBlog(Guid microId);
        Task<MicroBlog> GetMicroForUser(Guid userId, Guid microId);

        Task<bool> ExistsMicroBlog(Guid microId);

        //Task<MicroBlog> GetMicroBlogByMicroId(Guid microId);
    }
}