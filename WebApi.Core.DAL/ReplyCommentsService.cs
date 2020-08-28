using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class ReplyCommentsService:BaseService<ReplyComments>,IReplyCommentsService
    {
        public ReplyCommentsService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}