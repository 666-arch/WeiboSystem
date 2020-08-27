using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Core.DAL;
using WebApi.Core.IDAL;
using WebApi.Core.IManager;
using WebApi.Core.Manager;
using WebApi.Core.Models;

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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters() //允许存在 xml 数据格式请求，默认 Json
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
                            ContentTypes = {"application/problem+json"}
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

            var sqlConnection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<WeiBoDbContext>(options =>
            {
                options.UseSqlServer(sqlConnection);
            });
            //注册跨域访问服务
            services.AddCors(option => option.AddPolicy("MicroCore", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
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

            app.UseAuthorization();
            
            app.UseCors("MicroCore");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("MicroCore");
            });
        }
    }
}
