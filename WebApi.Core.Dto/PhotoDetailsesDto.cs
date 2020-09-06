using System;

namespace WebApi.Core.Dto
{
    public class PhotoDetailsesDto
    {
        public string ImagePaths { get; set; }
        public Guid? PhotoAlbumId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string PhotoAlbumName { get; set; }  //相册名称
        public bool PhotoPermissions { get; set; }  //相册权限 
    }
}