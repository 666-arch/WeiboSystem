using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Core.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User:BaseEntity
    {
        [Column(TypeName ="varchar(40)" )]
        public string RealName { get; set; }    //真实姓名
        [Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }    //昵称
        [Required, Column(TypeName = "varchar(40)")]
        public string Email { get; set; }   //邮箱
        [Required, Column(TypeName = "varchar(30)")]
        public string Password { get; set; }    //密码
        public Gender Gender { get; set; } = Gender.男;    //性别
        [Column(TypeName = "varchar(500)")]
        public string PersonalElucidation { get; set; } //个人说明
        [ Column(TypeName = "varchar(300)")]
        public string ImagePath { get; set; }   //个人头像
        public DateTime DateOfBirth { get; set; }   //出生日期
    }
}