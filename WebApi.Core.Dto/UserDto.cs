using System;

namespace WebApi.Core.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }    //昵称
        public string GenderDisplay { get; set; }    //性别
        public string PersonalElucidation { get; set; } //个人说明
        public string ImagePath { get; set; }   //个人头像
        public int Age { get; set; }   //出生日期
    }
}