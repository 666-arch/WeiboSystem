using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController:ControllerBase
    {
        private readonly IUserManager _user;
        private readonly IMapper _mapper;

        public UserController(IUserManager user,IMapper mapper)
        {
            _user = user ??
                    throw new ArgumentNullException(nameof(user));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> 
            GetUsers([FromQuery]UserDtoParameters userDtoParameters)
        {
            var users = _user.GetUsersAsync(userDtoParameters);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        public async Task<IActionResult>
            UserLogin(string username, string password)
        {
            if (!await _user.Login(username, password))
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("{id}",Name = nameof(GetUser))]
        public async Task<ActionResult<UserDto>> 
            GetUser(Guid id)
        {
            var user =await _user.GetUserAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>>
            CreateUser(UserAddDto user)
        {
            var entity = _mapper.Map<User>(user);
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _user.AddUser(entity);
            var dtosToReturn = _mapper.Map<UserDto>(entity);
            return CreatedAtRoute(nameof(GetUser), new {id = dtosToReturn.Id}, dtosToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> 
            UpdateUser(Guid id,UserUpdateDto user)
        {
            if (!await _user.UserExistsAsync(id))
            {
                return NotFound();
            }
            var entity =await _user.GetUserAsync(id);
            _mapper.Map(user, entity);
            await _user.EditUsers(entity);
            return NoContent();
        }
    }
}