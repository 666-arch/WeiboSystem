using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Core.Models;

namespace WebApi.Core.IDAL
{
    public interface IBaseService<T>:IDisposable where T:BaseEntity
    {
        Task CreateAsync(T model,bool save = true);
        Task EditAsync(T model, bool save = true);
        Task RemoveAsync(Guid id, bool save = true);
        Task RemoveAsync(T model, bool save = true);
        Task Save();
        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAllAsync();
    }
}
