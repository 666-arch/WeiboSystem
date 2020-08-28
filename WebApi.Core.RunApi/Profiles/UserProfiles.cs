using System;
using AutoMapper;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Profiles
{
    public class UserProfiles:Profile
    {
        public UserProfiles()
        {
            //源类型=>目标类型
            CreateMap<User, UserDto>()
                .ForMember(target => target.GenderDisplay,
                    opt => opt
                        .MapFrom(src => src.Gender.ToString()))
                .ForMember(target => target.Age,
                    opt => opt
                        .MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}