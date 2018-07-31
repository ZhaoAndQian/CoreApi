using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preoff.Entity;
using Preoff.Repository;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Preoff.Controllers
{
    /// <summary>
    /// 任务控制器
    /// </summary>
    //[Authorize]
    [Route("EventVideo")]
    [EnableCors("AllowAllOrigins")]
    public class EventVideoController : BaseController
    {
        /// <summary>
        /// 事件类型仓库
        /// </summary>
        public readonly IRepository<EventVideoTable> _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        private IHostingEnvironment hostingEnv;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        /// <param name="env">环境</param>
        public EventVideoController(IRepository<EventVideoTable> _db, IHostingEnvironment env)
        {
            _repository = _db;
            hostingEnv = env;
        }
        /// <summary>
        /// 根据事件id查询视频表
        /// </summary>
        /// <param name="id">事件id</param>
        /// <returns></returns>
        [HttpGet("select/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Json(new
                {
                    table = _repository.LoadListAll(p => p.EventTableId == id),
                    state = "0",
                    msg = "操作成功!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作!"
                });
            }

        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="id">视频id</param>
        /// <returns></returns>
        [HttpDelete("del/{id}")]
        public IActionResult Del(int id)
        {
            try
            {
                int count = _repository.Delete(p => p.Id == id);
                return Json(new
                {
                    count,
                    state = "0",
                    msg = "操作成功！"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }

        /// <summary>
        /// 上传视频[限制大小20M]
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="_files">视频文件</param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public IActionResult Post(int id, IFormFileCollection _files)
        {
            try
            {
                var files = Request.Form.Files[0];
                var fileExtension = Path.GetExtension(files.FileName);
                string fileFilt = ".mp4";


                if (fileExtension == null)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "上传的文件没有后缀!"
                    });
                }
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "上传的文件不是视频格式!"
                    });
                }


                long size = files.Length;
                var now = DateTime.Now;
                var filePathExt = now.ToString("yyyyMMdd");
                if (size > 20971420)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "文件大小不应超过20M！"
                    });
                }


                var fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');

                string filePath = $@"E:\corewebapi\Upload\Video\";

                if (!Directory.Exists(filePath + filePathExt))
                {
                    Directory.CreateDirectory(filePath + filePathExt);
                }

                fileName = Guid.NewGuid() + "." + fileName.Split('.')[1];

                string fileFullName = filePath + filePathExt + @"\" + fileName;

                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
                string message = $"上传成功!";
                EventVideoTable _video = new EventVideoTable
                {
                    EventTableId = id,
                    VideoPath = $@"\Upload\Video\{filePathExt}\{fileName}"
                };
                    return Json(new
                    {
                        id= _repository.SaveGetId(_video),
                        path = $@"\Upload\Video\{filePathExt}\{fileName}",
                        state = "0",
                        msg = message
                    });
            }

            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "上传失败!"
                });
            }
        }

        /// <summary>
        /// 根据视频id下载视频
        /// </summary>
        /// <param name="id">视频id</param>
        /// <returns></returns>
        [HttpGet("DownLoad")]
        public IActionResult Down(int id)
        {
            try
            {
                var pic = _repository.Get(p => p.Id == id);
                string basepath = @"E:\corewebapi";
                string contentType = "video/mp4";
                string path = (basepath + pic.VideoPath);
                var stream = System.IO.File.OpenRead(path);
                return File(stream, contentType);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "下载失败!"
                });
            }
        }
    }
}
