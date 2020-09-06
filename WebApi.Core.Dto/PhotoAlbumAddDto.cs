using System;

namespace WebApi.Core.Dto
{
    public class PhotoAlbumAddDto
    {
        public string PhotoAlbumName { get; set; }  //相册名称
        public bool PhotoPermissions { get; set; }  //相册权限 
    }
}