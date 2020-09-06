using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 微博表
    /// </summary>
    public class MicroBlog:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        [Required,Column(TypeName = "ntext")]
        public string MicroContent { get; set; }
        [Column(TypeName = "varchar(800)")]
        public string MicroImagePath { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string MicroVideo { get; set; }

        [Column(TypeName = "int")]
        public int MicroLikeCount { get; set; }    //总点赞数

    }
}