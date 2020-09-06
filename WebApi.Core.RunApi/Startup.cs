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
            //            ValidateIssuer = true,  //�Ƿ���֤Issuer
            //            ValidateAudience = true,
            //            ValidateLifetime = true,   //�Ƿ���֤ʧЧʱ��
            //            ClockSkew = TimeSpan.FromMinutes(5),
            //            ValidateIssuerSigningKey = true,
            //            ValidAudience = "https://localhost:5001",
            //            ValidIssuer = "https://localhost:5000",
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecurityKey"))
            //        };
            //    });
            services.AddAuthentication(options =>
            {
                //��֤middleware����
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                //��Ҫ��jwt  token��������
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //�䷢��
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtSetting:Issuer"],
                    //����Ȩ��
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtSetting:Audience"],
                    //��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:SecretKey"])),
                    //�Ƿ���֤ʧЧʱ�䡾ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Աȡ�
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)//����ķ�����ʱ��ƫ������5���ӡ�
                };
            });

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters() //������� xml ���ݸ�ʽ����Ĭ�� Json
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
                            ContentTypes = { "application/problem+json" }
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
            //ע�������ʷ���
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


            app.UseCors("MicroCore");

            app.UseAuthentication(); //��֤�м��

            app.UseAuthorization(); //��Ȩ�м��


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("MicroCore");
            });
        }
    }
}
