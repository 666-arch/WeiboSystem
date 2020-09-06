using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Dto;

namespace WebApi.Core.RunApi.Helpers
{
    public class PhotoFileHelper
    {
        public  PhotoFileHelper(PhotoDetailsesAddDto photoDetailses,List<IFormFile> files)
        {
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
                             item.CopyTo(fs);
                             fs.Flush();
                        }
                        filenames.Add(filePath + saveName);

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}