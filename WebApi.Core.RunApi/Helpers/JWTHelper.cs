using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebApi.Core.RunApi.Helpers
{
    public class JWTHelper
    {
        private readonly IConfiguration _configuration;

        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResultHelper GetToken(string Email)
        {
            //相关Token的常量
            var claims = new[]
            {
                 new Claim(ClaimTypes.SerialNumber, Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //JWT规定的部分字段
            var token = new JwtSecurityToken
            (
                    issuer: _configuration["JwtSetting:Issuer"], //提供者
                    audience: _configuration["JwtSetting:Audience"], //被授权者
                    claims: claims,
                    expires: DateTime.Now.AddHours(24), //过期时间 24h
                    signingCredentials: creds
            );
            DateTime time = token.ValidTo;
            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            TokenResultHelper tokenResult=new TokenResultHelper
            {
                token = Token,
                time = time
            };
            return tokenResult;
        }
    }
}