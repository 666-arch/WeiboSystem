using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class PhotoDetailseService:BaseService<PhotoDetails>,IPhotoDetailseService
    {
        public PhotoDetailseService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}