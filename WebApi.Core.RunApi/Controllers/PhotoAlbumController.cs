using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.IManager;

namespace WebApi.Core.RunApi.Controllers
{
    public class PhotoAlbumController:ControllerBase
    {
        private readonly IPhotoAlbumManager _manager;
        private readonly IMapper _mapper;

        public PhotoAlbumController(IPhotoAlbumManager manager,IMapper mapper)
        {
            _manager = manager ?? 
                       throw new ArgumentNullException(nameof(manager));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
        }


    }
}