using AutoMapper;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Profiles
{
    public class MicroBlogProfiles:Profile
    {
        public MicroBlogProfiles()
        {
            CreateMap<MicroBlog, MicroBlogDto>();

            CreateMap<MicroBlogAddDto, MicroBlog>();

        }
    }
}