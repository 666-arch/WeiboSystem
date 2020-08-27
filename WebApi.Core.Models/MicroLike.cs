using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 微博点赞表
    /// </summary>
    public class MicroLike:BaseEntity
    {
        [ForeignKey(nameof(MicroBlog))]
        public Guid? MicroBlogId { get; set; }
        public MicroBlog MicroBlog { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public bool LikeSign { get; set; }
    }
}