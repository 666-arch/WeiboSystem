using System;

namespace WebApi.Core.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public DateTime CreateTime { get; set; }=DateTime.Now;
        public bool IsRemoved { get; set; }
    }
}