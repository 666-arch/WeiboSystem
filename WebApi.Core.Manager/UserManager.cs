using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Dto;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.Manager
{
    public class UserManager:IUserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Register(string email, string password)
        {
            await _userService.CreateAsync(new User()
            {
                
            });
        }

        public async Task AddUser(User user)
        {
            if (user==null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.DateOfBirth=DateTime.Now;
            user.PersonalElucidation = "暂无";
            await _userService.CreateAsync(user);
        }

        public async Task<bool> Login(string email, string password)
        {
            return await _userService.GetAllAsync()
                .AnyAsync(x => x.Email == email && x.Password == password);
        }
        public async Task EditUser(string email, string username, string realname, string imagepath)
        {
            throw new NotImplementedException();
        }

        public async Task EditUsers(User user)
        {
            if (user==null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _userService.EditAsync(user);
        }

        public IQueryable<User> GetUsersAsync(UserDtoParameters parameters)
        {
            if (parameters==null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (string.IsNullOrWhiteSpace(parameters.UserName)
                &&string.IsNullOrWhiteSpace(parameters.SearchName))
            {
                return  _userService.GetAllAsync();
            }
            var queryUser = _userService.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(parameters.UserName))
            {
                parameters.UserName = parameters.UserName.Trim();
                queryUser = queryUser.Where(x => x.UserName == parameters.UserName);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchName))
            {
                parameters.SearchName = parameters.SearchName.Trim();
                queryUser = queryUser
                    .Where(x => x.Email.Contains(parameters.SearchName) || 
                                x.RealName.Contains(parameters.SearchName));
            }
            return queryUser;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            if (id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _userService.GetAllAsync().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            if (id==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _userService.GetAllAsync().AnyAsync(x => x.Id == id);
        }
    }
}
