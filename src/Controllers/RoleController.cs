using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preoff.Entity;
using Preoff.Repository;

namespace Preoff.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Role")]
    [EnableCors("AllowAllOrigins")]
    public class RoleController : BaseController
    {
        /// <summary>
        /// 无人机类型仓库
        /// </summary>
        public readonly IRepository<RoleTable> _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public RoleController(IRepository<RoleTable> _db)
        {
            _repository = _db;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]RoleTable role)
        {
            try
            {
                int id = _repository.SaveGetId(role);
                return Json(new
                {
                    id,
                    state = "0",
                    msg = "添加成功！"
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
        /// 更新[所有字段,支持批量]
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns></returns>
        [HttpPost("UpdateList")]
        public IActionResult UpdateList([FromBody]List<RoleTable> list)
        {
            try
            {
                int count = _repository.UpdateList(list);
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
        /// 删除指定Id
        /// </summary>
        /// <param name="id">无人机类型ID</param>
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
        /// 查询所有
        /// </summary>
        /// <returns>返回所有</returns>
        [HttpGet("selectall")]
        public IActionResult SelectAll()
        {
            try
            {
                return Json(new
                {
                    table = _repository.LoadListAll(),
                    state = "0",
                    msg = "操作成功!"
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

    }
}