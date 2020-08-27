using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class MicroBlogService:BaseService<MicroBlog>,IMicroBlogService
    {
        public MicroBlogService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}