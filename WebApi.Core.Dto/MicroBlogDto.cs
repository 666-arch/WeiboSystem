using System;

namespace WebApi.Core.Dto
{
    public class MicroBlogDto
    {
        public Guid? UserId { get; set; }
        public string MicroContent { get; set; }
        public string MicroImagePath { get; set; }
        public string MicroVideo { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}