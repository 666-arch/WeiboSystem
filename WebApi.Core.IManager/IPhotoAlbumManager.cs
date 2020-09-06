using System;
using System.Threading.Tasks;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.IManager
{
    public interface IPhotoAlbumManager
    {
        Task CreatePhotoAlbum(Guid userId, PhotoAlbum photo);
        Task<PhotoAlbumDto> GetPhotoAlbum(Guid userId);

        Task<bool> ExistsPhotoAlbum(Guid albumId);
    }
}