using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 微博评论表
    /// </summary>
    public class MicroComments:BaseEntity
    {
        [Required,Column(TypeName = "varchar(500)")]
        public string CommentsContent { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(MicroBlog))]
        public Guid? MicroBlogId { get; set; }
        public MicroBlog MicroBlog { get; set; }
    }
}