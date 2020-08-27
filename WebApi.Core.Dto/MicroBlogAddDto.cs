using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class MicroBlogAddDto
    {
        [Display(Name = "微博内容")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string MicroContent { get; set; }
        public string MicroImagePath { get; set; }
        public string MicroVideo { get; set; }
    }
}