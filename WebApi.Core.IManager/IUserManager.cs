using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Core.Dto;
using WebApi.Core.Models;

namespace WebApi.Core.IManager
{
    public interface IUserManager
    {
        Task Register(string email, string password);

        Task AddUser(User user);

        Task<bool> Login(string email, string password);

        Task EditUser(string email, string username, string realname,string imagepath);
        Task EditUsers(User user);
        IQueryable<User> GetUsersAsync(UserDtoParameters parameters);

        Task<User> GetUserAsync(Guid id);

        Task<bool> UserExistsAsync(Guid id);

    }
}
