using System;

namespace WebApi.Core.Dto
{
    public class PhotoAlbumDto
    {
        public string PhotoAlbumName { get; set; }  //相册名称
        public bool PhotoPermissions { get; set; }  //相册权限 
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}