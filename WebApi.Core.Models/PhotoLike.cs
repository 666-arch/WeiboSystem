using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 相册点赞表
    /// </summary>
    public class PhotoLike:BaseEntity
    {
        [ForeignKey(nameof(PhotoAlbum))]
        public Guid? PhotoAlbumId { get; set; }
        public PhotoAlbum PhotoAlbum { get; set; }
        public bool LikeSign { get; set; }  //点赞标识
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}