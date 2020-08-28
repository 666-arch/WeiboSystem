using AutoMapper;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Profiles
{
    public class ReplyCommentProfiles:Profile
    {
        public ReplyCommentProfiles()
        {
            CreateMap<ReplyCommentsAddDto, ReplyComments>();

            CreateMap<ReplyComments, ReplyCommentsDto>()
                .ForMember(target=>target.ByReplyNickName,
                    opt=>opt
                        .MapFrom(src=>src.Users.UserName));
        }   
    }
}