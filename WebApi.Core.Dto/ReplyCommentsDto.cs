using System;

namespace WebApi.Core.Dto
{
    public class ReplyCommentsDto
    {
        public string ReplyContent { get; set; }
        public Guid? RespondUserId { get; set; }
        public Guid? RespondTargetUserId { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public string ByReplyNickName { get; set; }
        public Guid? MicroCommentId { get; set; }//针对评论的回复
        public Guid ReplyToTargetCommentId { get; set; }//针对回复的回复
        public DateTime CreateTime { get; set; }

    }
}