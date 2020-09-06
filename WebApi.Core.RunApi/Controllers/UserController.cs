using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.Models;
using WebApi.Core.RunApi.Helpers;
using WebApi.Core.RunApi.ViewModel;

namespace WebApi.Core.RunApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize] //调用前须身份认证
    public class UserController:ControllerBase
    {
        private readonly IUserManager _user;
        private readonly IMapper _mapper;
        private readonly JWTHelper _helper;

        public UserController(IUserManager user,
            IMapper mapper,
            JWTHelper helper)
        {
            _user = user ??
                    throw new ArgumentNullException(nameof(user));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
            _helper = helper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> 
            GetUsers([FromQuery]UserDtoParameters userDtoParameters)
        {
            var users = _user.GetUsersAsync(userDtoParameters);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [AllowAnonymous]    //登录接口无需身份认证
        [HttpPost]
        public async Task<IActionResult>
            UserLogin(UserLoginViewModel user)
        {
            if (!await _user.Login(user.Email, user.Password))
            {
                return NotFound();
            }
            TokenResultHelper token = _helper.GetToken(user.Email);
            return Ok(new
            {
                token.time,
                token.token
            });
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