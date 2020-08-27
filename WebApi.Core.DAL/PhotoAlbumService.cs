using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class PhotoAlbumService:BaseService<PhotoAlbum>,IPhotoAlbumService
    {
        public PhotoAlbumService(WeiBoDbContext context)
            :base(context)
        {
            
        }
    }
}