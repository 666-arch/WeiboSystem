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
            }).AddXmlDataContractSerializerFormatters() //������� xml ���ݸ�ʽ����Ĭ�� Json
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "",
                            Title = "���ʹ���",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "��鿴��ϸ��Ϣ",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = {"application/problem+json"}
                        };
                    };
                });

            //ע��Mapper����
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //ע��ӿ���������
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
            //ע�������ʷ���
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
                //�Զ��� 500 ��Ϣ����
                app.UseExceptionHandler(appBulider =>
                {
                    appBulider.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("���������ִ���,���Ժ����ԣ�");
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
