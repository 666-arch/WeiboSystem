using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class MicroCommentsDto
    {
        public string CommentsContent { get; set; }
        public string UserName { get; set; }    //评论人
        public string ImagePath { get; set; }
        public Guid? UserId { get; set; }
        public Guid? MicroBlogId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}