using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class UserService:BaseService<User>,IUserService
    {
        public UserService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}