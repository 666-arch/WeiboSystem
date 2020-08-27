using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    public class MicroFans:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }  //当前的用户,根据当前用户Id可以找我关注的Id
        public User User { get; set; }

        [ForeignKey(nameof(FocusUser))]
        public Guid? FocusUserId { get; set; }  //关注的用户Id
        public User FocusUser { get; set; }
    }
}