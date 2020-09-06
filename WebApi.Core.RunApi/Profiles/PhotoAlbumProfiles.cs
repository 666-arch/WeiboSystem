using AutoMapper;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Profiles
{
    public class PhotoAlbumProfiles:Profile
    {
        public PhotoAlbumProfiles()
        {
            CreateMap<PhotoAlbumAddDto, PhotoAlbum>();
            CreateMap<PhotoAlbum, PhotoAlbumDto>();
        }
    }
}