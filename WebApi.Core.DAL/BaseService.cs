using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.IDAL;
using WebApi.Core.Models;

namespace WebApi.Core.DAL
{
    public class BaseService<T>:IBaseService<T> where T:BaseEntity,new()
    {

        private readonly WeiBoDbContext _context;
        public BaseService(WeiBoDbContext context)
        {
            _context = context ?? 
                       throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateAsync(T model, bool save = true)
        {
            if (model==null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            await _context.Set<T>().AddAsync(model);
           if (save)
           {
               await _context.SaveChangesAsync();
           }
        }
        public async Task EditAsync(T model,bool save = true)
        {
            if (model==null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _context.Entry(model).State = EntityState.Modified;
            if (save)
            {
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveAsync(Guid id, bool save = true)
        {
            if (id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var t = new T()
            {
                Id = id
            };
            _context.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (save)
            {
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveAsync(T model, bool save = true)
        {
            if (model==null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await RemoveAsync(model.Id,save);
        }
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
        public async Task<T> GetOneByIdAsync(Guid id)
        {
            if (id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await GetAllAsync().FirstAsync(x => x.Id == id);
        }
        public IQueryable<T> GetAllAsync()
        {
            return  _context.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
