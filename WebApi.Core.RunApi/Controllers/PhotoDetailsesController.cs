using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.RunApi.Helpers;

namespace WebApi.Core.RunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PhotoDetailsesController:ControllerBase
    {
        private readonly IPhotoDetailseManager _manager;
        private readonly IUserManager _user;
        private readonly IPhotoAlbumManager _album;
        private readonly IMapper _mapper;

        public PhotoDetailsesController(IPhotoDetailseManager manager,
            IUserManager user,
            IPhotoAlbumManager album,
            IMapper mapper)
        {
            _manager = manager 
                       ?? throw new ArgumentNullException(nameof(manager));
            _user = user 
                    ?? throw new ArgumentNullException(nameof(user));
            _album = album 
                     ?? throw new ArgumentNullException(nameof(album));
            _mapper = mapper 
                      ?? throw new ArgumentNullException(nameof(mapper));
        }
        //[HttpPost("{userid}/{albumId}")]
        //public async Task<ActionResult<PhotoDetailsesDto>> 
        //    CreatePhotoDetailes(Guid userId,
        //        Guid albumId,
        //        PhotoDetailsesAddDto photoDetailses,
        //        List<IFormFile> files)
        //{
        //    if (! await _user.UserExistsAsync(userId))
        //    {
        //        return NotFound();
        //    }
        //    if (!await _album.ExistsPhotoAlbum(albumId))
        //    {
        //        return NotFound();
        //    }
        //    if (photoDetailses==null)
        //    {
        //        return NotFound();
        //    }
        //    //PhotoFileHelper photoFile=new PhotoFileHelper(files);

        //}
    }
}