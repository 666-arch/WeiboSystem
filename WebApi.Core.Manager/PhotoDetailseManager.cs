using System;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;

namespace WebApi.Core.Manager
{
    public class PhotoDetailseManager:IPhotoDetailseManager
    {
        private readonly IPhotoDetailseService _photoDetailse;

        public PhotoDetailseManager(IPhotoDetailseService photoDetailse)
        {
            _photoDetailse = photoDetailse 
                             ?? throw new ArgumentNullException(nameof(photoDetailse));
        }
    }
}