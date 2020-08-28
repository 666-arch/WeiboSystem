using AutoMapper;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Profiles
{
    public class MicroCommentProfiles:Profile
    {
        public MicroCommentProfiles()
        {
            CreateMap<MicroCommentsAddDto, MicroComments>();
            CreateMap<MicroComments, MicroCommentsDto>();
        }
    }
}