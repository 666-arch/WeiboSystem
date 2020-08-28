using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class MicroCommentsAddDto
    {
        [Display(Name = "评论内容")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string CommentsContent { get; set; }
        public Guid? UserId { get; set; }
        public Guid? MicroBlogId { get; set; }
    }
}