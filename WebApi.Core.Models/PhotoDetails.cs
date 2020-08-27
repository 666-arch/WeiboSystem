using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 相册详情表
    /// </summary>
    public class PhotoDetails:BaseEntity
    {
        [Required,Column(TypeName = "varchar(300)")]
        public string ImagePaths { get; set; }
        [ForeignKey(nameof(PhotoAlbum))]
        public Guid? PhotoAlbumId { get; set; }
        public PhotoAlbum PhotoAlbum { get; set; }
    }
}