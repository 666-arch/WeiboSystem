using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class UserAddDto
    {
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "{0}不可为空")]
        public string Email { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [MaxLength(30,ErrorMessage = "{0}最大长度不能超过{1}个字符")]
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}