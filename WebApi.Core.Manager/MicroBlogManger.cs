using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Core.Dto;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.IManager.Helper;
using WebApi.Core.Models;

namespace WebApi.Core.Manager
{
    public class MicroBlogManger:IMicroBlogManger
    {
        private readonly IMicroBlogService _micro;

        public MicroBlogManger(IMicroBlogService micro)
        {
            _micro = micro;
        }
        public async Task CreateMicroBlog(Guid userId, MicroBlog micro)
        {
            if (micro==null)
            {
                throw new ArgumentNullException(nameof(micro));
            }
            micro.UserId = userId;
            await _micro.CreateAsync(micro);
        }

        public async Task<PageList<MicroBlogDto>> GetMicroBlogs(MicroBlogDtoParameters microBlogDtoParameters)
        {
            if (microBlogDtoParameters==null)
            {
                throw new ArgumentNullException(nameof(microBlogDtoParameters));
            }
            //if (string.IsNullOrWhiteSpace(microBlogDtoParameters.MicroContent)&&
            //    string.IsNullOrWhiteSpace(microBlogDtoParameters.SearchQuery))
            //{
            //    return await _micro.GetAllAsync().Select(m => new MicroBlogDto()
            //    {
            //        UserName = m.User.UserName,
            //        MicroContent = m.MicroContent,
            //        UserId = m.UserId,
            //        MicroVideo = m.MicroVideo,
            //        MicroImagePath = m.MicroImagePath,
            //        CreateTime = m.CreateTime
            //    }).OrderByDescending(m=>m.CreateTime) 
            //        .ToListAsync();
            //}

            var queryMicroItem = _micro.GetAllAsync()
                .Select(m=>new MicroBlogDto()
                {
                    UserName = m.User.UserName,
                    MicroContent = m.MicroContent,
                    UserId = m.UserId,
                    MicroVideo = m.MicroVideo,
                    MicroImagePath = m.MicroImagePath,
                    CreateTime = m.CreateTime
                });
            
            if (!string.IsNullOrWhiteSpace(microBlogDtoParameters.MicroContent))
            {
                microBlogDtoParameters.MicroContent = microBlogDtoParameters.MicroContent.Trim();
                queryMicroItem = queryMicroItem
                    .Where(x => x.MicroContent == microBlogDtoParameters.MicroContent)
                    .OrderByDescending(m => m.CreateTime);
            }
            if (!string.IsNullOrWhiteSpace(microBlogDtoParameters.SearchQuery))
            {
                microBlogDtoParameters.SearchQuery = microBlogDtoParameters.SearchQuery.Trim();
                queryMicroItem = queryMicroItem.Where(x =>
                    x.UserName.Contains(microBlogDtoParameters.SearchQuery) ||
                    x.MicroContent.Contains(microBlogDtoParameters.SearchQuery))
                    .OrderByDescending(m => m.CreateTime);
            }

            //分页逻辑
            //queryMicroItem = queryMicroItem
            //    .Skip(microBlogDtoParameters.PageSize * 
            //          (microBlogDtoParameters.PageNumber - 1))
            //    .Take(microBlogDtoParameters.PageSize);

            return await PageList<MicroBlogDto>
                .CreateAsync(queryMicroItem, microBlogDtoParameters.PageNumber, microBlogDtoParameters.PageSize);
        }

        public async Task<IEnumerable<MicroBlogDto>> GetMicroBlogForUser(Guid userId)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return await _micro.GetAllAsync()
                .Where(x => x.UserId == userId).Select(m=>new MicroBlogDto()
                {
                    UserName = m.User.UserName,
                    MicroContent = m.MicroContent,
                    UserId = m.UserId,
                    MicroVideo = m.MicroVideo,
                    MicroImagePath = m.MicroImagePath,
                    CreateTime = m.CreateTime
                }).OrderByDescending(x=>x.CreateTime)
                .ToListAsync();
        }

        public async Task RemoveMicroBlog(Guid microId)
        {
            if (microId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(microId));
            }
            await _micro.RemoveAsync(microId);
        }

        public async Task<MicroBlog> GetMicroForUser(Guid userId, Guid microId)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (microId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(microId));
            }

            return await _micro.GetAllAsync().FirstAsync(x => x.UserId == userId & x.Id == microId);
        }
    }
}