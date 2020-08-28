using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class ReplyCommentsAddDto
    {
        [Required(ErrorMessage = "请输入回复内容")]
        public string ReplyContent { get; set; }
        public Guid? RespondUserId { get; set; }
        public Guid? RespondTargetUserId { get; set; }
        public int ReplyType { get; set; }
        public Guid? MicroCommentId { get; set; }
        public Guid ReplyToTargetCommentId { get; set; }
    }
}