using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 评论回复表
    /// </summary>
    public class ReplyComments:BaseEntity
    {
        [Required, Column(TypeName = "varchar(500)")]
        public string ReplyContent { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? RespondUserId { get; set; }     //回复用户Id
        public User User { get; set; }

        [ForeignKey(nameof(Users))]
        public Guid? RespondTargetUserId { get; set; }   //回复目标用户Id
        public User Users { get; set; }
        public int ReplyType { get; set; }      //回复类型
        [ForeignKey(nameof(MicroComments))]
        public Guid? MicroCommentId { get; set; }    //针对评论的回复
        public MicroComments MicroComments { get; set; }
        public Guid ReplyToTargetCommentId { get; set; }    //针对回复的回复
    }
}