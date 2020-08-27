using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class UserUpdateDto
    {
        [Display(Name = "个人说明")]
        [Required(ErrorMessage = "{0}不可为空")]
        public string PersonalElucidation { get; set; }

        [Display(Name = "出生日期")]
        [Required(ErrorMessage = "请选择您的{0}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "性别")]
        [Required(ErrorMessage = "请选择您的{0}")]
        public string GenderDisplay { get; set; }

        [Display(Name = "昵称")]
        [Required(ErrorMessage = "请输入输入您的{0}")]
        public string UserName { get; set; }

        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = "请输入输入您的{0}")]
        public string RealName { get; set; }
    }
}