using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class MicroCommentsService:BaseService<MicroComments>,IMicroCommentsService
    {
        public MicroCommentsService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}