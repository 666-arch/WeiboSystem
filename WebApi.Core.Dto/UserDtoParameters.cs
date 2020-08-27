namespace WebApi.Core.Dto
{
    /// <summary>
    /// 处理过滤和搜索类
    /// </summary>
    public class UserDtoParameters
    {
        public string UserName { get; set; }
        public string SearchName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}