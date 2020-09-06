using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using WebApi.Core.DAL;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.Manager;
using WebApi.Core.Models;
using WebApi.Core.RunApi.Helpers;

namespace WebApi.Core.RunApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(option =>
            //    {
            //        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,  //是否验证Issuer
            //            ValidateAudience = true,
            //            ValidateLifetime = true,   //是否验证失效时间
            //            ClockSkew = TimeSpan.FromMinutes(5),
            //            ValidateIssuerSigningKey = true,
            //            ValidAudience = "https://localhost:5001",
            //            ValidIssuer = "https://localhost:5000",
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecurityKey"))
            //        };
            //    });
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                //主要是jwt  token参数设置
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //颁发者
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtSetting:Issuer"],
                    //被授权者
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtSetting:Audience"],
                    //秘钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:SecretKey"])),
                    //是否验证失效时间【使用当前时间与Token的Claims中的NotBefore和Expires对比】
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)//允许的服务器时间偏移量【5分钟】
                };
            });

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters() //允许存在 xml 数据格式请求，默认 Json
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "",
                            Title = "发送错误",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请查看详细信息",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            //注册Mapper服务
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //注册接口生命周期
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IMicroBlogService, MicroBlogService>();
            services.AddScoped<IMicroBlogManger, MicroBlogManger>();

            services.AddScoped<IPhotoAlbumService, PhotoAlbumService>();
            services.AddScoped<IPhotoAlbumManager, PhotoAlbumManager>();

            services.AddScoped<IMicroCommentsService, MicroCommentsService>();
            services.AddScoped<IMicroCommentsManager, MicroCommentsManager>();

            services.AddScoped<IReplyCommentsService, ReplyCommentsService>();
            services.AddScoped<IReplyCommentsManager, ReplyCommentsManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<JWTHelper>();

            var sqlConnection = Configuration
                .GetConnectionString("SqlServerConnection");
            services.AddDbContext<WeiBoDbContext>(options =>
            {
                options.UseSqlServer(sqlConnection);
            });
            //注册跨域访问服务
            services.AddCors(option => option
                .AddPolicy("MicroCore", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
            services.AddSingleton<IConnectionMultiplexer>
                (ConnectionMultiplexer.Connect("localhost"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //自定义 500 信息机制
                app.UseExceptionHandler(appBulider =>
                {
                    appBulider.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("服务器出现错误,请稍后再试！");
                    });
                });
            }
            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors("MicroCore");

            app.UseAuthentication(); //认证中间件

            app.UseAuthorization(); //授权中间件


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("MicroCore");
            });
        }
    }
}
