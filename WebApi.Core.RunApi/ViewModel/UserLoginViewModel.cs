using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.RunApi.ViewModel
{
    public class UserLoginViewModel
    {
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Email { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password { get; set; }
    }
}