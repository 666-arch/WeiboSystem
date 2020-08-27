using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 相册收藏表
    /// </summary>
    public class PhotoCollection:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(PhotoAlbum))]
        public Guid? PhotoAlbumId { get; set; }
        public PhotoAlbum PhotoAlbum { get; set; }
    }
}