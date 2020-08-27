using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Dto
{
    public class MicroBlogDtoParameters
    {
        private const int MaxPageSize = 20; //最大每页20笔

        public string MicroContent { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 2;
        //如果数据是 1000 或者是 10000
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

        }
    }
}