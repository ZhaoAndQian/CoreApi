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
    /// 权限菜单
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Permisson")]
    [EnableCors("AllowAllOrigins")]
    public class PermissonController : Controller
    {
        /// <summary>
        /// 仓储
        /// </summary>
        public readonly IRepository<PermissonTable> _repository;
        /// <summary>
        /// 用户角色映射仓储
        /// </summary>
        public readonly IRepository<UserRoleTable> _userRoleRepository;
        /// <summary>
        /// 角色菜单映射仓储
        /// </summary>
        public readonly IRepository<RolePermissionTable> _rolePRepository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="_db">注入数据仓储</param>
        /// <param name="roleRepository">注入数据仓储</param>
        /// <param name="rolePRepository">注入数据仓储</param>
        public PermissonController(IRepository<PermissonTable> _db, IRepository<UserRoleTable> roleRepository, IRepository<RolePermissionTable> rolePRepository)
        {
            _repository = _db;
            _userRoleRepository = roleRepository;
            _rolePRepository = rolePRepository;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="role">菜单表</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]PermissonTable role)
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
        public IActionResult UpdateList([FromBody]List<PermissonTable> list)
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
        /// <param name="id">id</param>
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
        /// 获取用户菜单
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>用户菜单</returns>
        [HttpPost("getUserPermisson")]
        public IActionResult GetUserPermisson([FromBody]UserTable user)
        {
           var roleList= _userRoleRepository.LoadAll().Where(t => t.UserTableId == user.Id).ToList();
           var perList=_rolePRepository.LoadAll(t=>roleList.Exists(f=>f.RoleTableId==t.RoleTableId)).ToList();
           var nodes = _repository.LoadAll(t=>perList.Exists(f=>f.PermissonTableId==t.Id)).ToList();
           var pids = (from n in nodes select n.PId).ToList();
           var parentNodes = _repository.LoadAll(t => pids.Contains(t.Id)).OrderBy(t=>t.PermissonSeq).ToList();
            foreach (var node in parentNodes)
            {
                node.Childrens = nodes.FindAll(t => t.PId == node.Id).OrderBy(t => t.PermissonSeq).OrderBy(t=>t.PermissonSeq).ToList();
            }
            return Json(new
            {
                table = parentNodes,
                state = "0",
                msg = "操作成功!"
            });
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns>返回所有（带父子关系）</returns>
        [HttpGet("selectTree")]
        public IActionResult SelectTree()
        {
            try
            {
                var nodes = _repository.LoadListAll();
                var parentNodes = nodes.FindAll(t => t.PId == 0).OrderBy(t => t.PermissonSeq);
                foreach (var node in parentNodes)
                {
                    node.Childrens = nodes.FindAll(t => t.PId == node.Id).OrderBy(t => t.PermissonSeq).ToList();
                }
                return Json(new
                {
                    table = parentNodes,
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