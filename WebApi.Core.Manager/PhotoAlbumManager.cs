using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Dto;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.Manager
{
    public class PhotoAlbumManager:IPhotoAlbumManager
    {
        private readonly IPhotoAlbumService _photoAlbum;

        public PhotoAlbumManager(IPhotoAlbumService photoAlbum)
        {
            _photoAlbum = photoAlbum ?? 
                          throw new ArgumentNullException(nameof(photoAlbum));
        }

        public async Task CreatePhotoAlbum(Guid userId, PhotoAlbum photo)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (photo==null)
            {
                throw new ArgumentNullException(nameof(photo));
            }
            photo.UserId = userId;
            await _photoAlbum.CreateAsync(photo);
        }

        public async Task<PhotoAlbumDto> GetPhotoAlbum(Guid userId)
        {
            if (userId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return await _photoAlbum.GetAllAsync()
                .Where(x => x.UserId == userId)
                .Select(m => new PhotoAlbumDto()
                {
                    PhotoAlbumName=m.PhotoAlbumName,
                    PhotoPermissions = m.PhotoPermissions,
                    UserName = m.User.UserName,
                    UserId = m.UserId,
                    CreateTime=m.CreateTime
                }).OrderByDescending(x=>x.CreateTime)
                    .FirstAsync();
        }

        public async Task<bool> ExistsPhotoAlbum(Guid albumId)
        {
            if (albumId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(albumId));
            }

            return await _photoAlbum.GetAllAsync().AnyAsync(x => x.Id == albumId);
        }
    }
}