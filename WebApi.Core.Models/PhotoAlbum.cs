using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 相册表
    /// </summary>
    public class PhotoAlbum:BaseEntity
    {
        [Required,Column(TypeName = "varchar(40)")]
        public string PhotoAlbumName { get; set; }  //相册名称
        public bool PhotoPermissions { get; set; } //相册权限 
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}