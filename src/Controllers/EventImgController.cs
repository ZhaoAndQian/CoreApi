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
    [Route("EventImg")]
    [EnableCors("AllowAllOrigins")]
    public class EventImgController : BaseController
    {
        /// <summary>
        /// 事件类型仓库
        /// </summary>
        public readonly IRepository<EventImgTable> _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        private IHostingEnvironment hostingEnv;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        /// <param name="env">环境</param>
        public EventImgController(IRepository<EventImgTable> _db, IHostingEnvironment env)
        {
            _repository = _db;
            hostingEnv = env;
        }
        /// <summary>
        /// 根据事件id查询图片表
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
                    table=_repository.LoadListAll(p => p.EventTableId == id),
                    state = "0",
                    msg = "操作成功!"
                });
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

        /// <summary>
        /// 删除时间图片
        /// </summary>
        /// <param name="id">图片id</param>
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
        /// 上传图片[限制大小10M]
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="_files">图片文件</param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public IActionResult Post(int id, IFormFileCollection _files)
        {
            try
            {
                var files = Request.Form.Files[0];
                var fileExtension = Path.GetExtension(files.FileName);
                string fileFilt = ".jpg";
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
                        msg = "上传的文件不是图片格式!"
                    });
                }
                long size = files.Length;
                var now = DateTime.Now;
                var filePathExt = now.ToString("yyyyMMdd");
                if (size > 10485760)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "文件大小不应超过10M！"
                    });
                }
                var fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                string filePath = $@"E:\corewebapi\Upload\Img\";
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
                EventImgTable _img = new EventImgTable
                {
                    EventTableId = id,
                    ImgPath = $@"\Upload\Img\{filePathExt}\{fileName}"
                };
                return Json(new
                {
                    id = _repository.SaveGetId(_img),
                    path = $@"\Upload\Img\{filePathExt}\{fileName}",
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
        /// 根据图片id下载图片
        /// </summary>
        /// <param name="id">图片id</param>
        /// <returns></returns>
        [HttpGet("DownLoad")]
        public IActionResult Down(int id)
        {
            try
            {
                var pic = _repository.Get(p => p.Id == id);
                string basepath = @"E:\corewebapi";
                string contentType = "image/jpg";
                string path = (basepath + pic.ImgPath);
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
