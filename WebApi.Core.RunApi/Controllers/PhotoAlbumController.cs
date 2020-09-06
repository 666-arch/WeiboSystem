using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Dto;
using WebApi.Core.IManager;
using WebApi.Core.Models;

namespace WebApi.Core.RunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PhotoAlbumController:ControllerBase
    {
        private readonly IPhotoAlbumManager _manager;
        private readonly IUserManager _user;
        private readonly IMapper _mapper;
        public PhotoAlbumController(IPhotoAlbumManager manager,IUserManager user,IMapper mapper)
        {
            _manager = manager ?? 
                       throw new ArgumentNullException(nameof(manager));
            _user = user ?? 
                    throw new ArgumentNullException(nameof(user));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<PhotoAlbumDto>> 
            CreatePhotoForUser(Guid userId,PhotoAlbumAddDto photo)
        {
            if (!await _user.UserExistsAsync(userId))
            {
                return NotFound();
            }
            var entity= _mapper.Map<PhotoAlbum>(photo);
            await _manager.CreatePhotoAlbum(userId,entity);
            var dtoToReturn = _mapper.Map<PhotoAlbumDto>(entity);
            return CreatedAtRoute(nameof(GetPhotoAlbum),
                new{ userId =dtoToReturn.UserId},
                dtoToReturn);
        }

        [HttpGet("{userId}",Name = nameof(GetPhotoAlbum))]
        public async Task<ActionResult<IEnumerable<PhotoAlbumDto>>> 
            GetPhotoAlbum(Guid userId)
        {
             var dto= await _manager.GetPhotoAlbum(userId);
             return Ok(dto);
        }



        [HttpPost("userid")]
        public async Task<OkObjectResult> FileSave(Guid userid,List<IFormFile> files)
        {
            if (!await _user.UserExistsAsync(userid))
            {
                return Ok("该用户不存在着");
            }

            if (files.Count < 1)
            {
                return Ok("空文件");
            }
            //返回的文件地址
            List<string> filenames = new List<string>();
            DateTime now = DateTime.Now;
            //文件存储路径
            string filePath = $"/File/{now:yyyy}/{now:yyyyMM}/{now:yyyyMMdd}/";
            //获取当前web目录
            var webRootPath = "File/"; ;
            if (!Directory.Exists(webRootPath + filePath))
            {
                Directory.CreateDirectory(webRootPath + filePath);
            }
            try
            {
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        //文件后缀
                        string fileExtension = Path.GetExtension(item.FileName);

                        //判断后缀是否是图片
                        const string fileFilt = ".gif|.jpg|.jpeg|.png";
                        if (fileExtension == null)
                        {
                            break;
                            //return Error("上传的文件没有后缀");
                        }
                        if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                        {
                            break;
                            //return Error("请上传jpg、png、gif格式的图片");
                        }
                        //判断文件大小
                        long length = item.Length;
                        if (length > 1024 * 1024 * 2) //2M
                        {
                            break;
                            //return Error("上传的文件不能大于2M");
                        }
                        string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                        string strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                        string saveName = strDateTime + strRan + fileExtension;
                        //插入图片数据
                        using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                        {
                            await item.CopyToAsync(fs);
                            await fs.FlushAsync();
                        }
                        filenames.Add(filePath + saveName);
                    }
                }
                
                return Ok(filenames);
            }
            catch (Exception ex)
            {
                return Ok("上传失败");
            }
        }
    }
}