using System;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;

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


    }
}