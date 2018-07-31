using DynamicExpresso;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Preoff.Comm;
using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using Preoff.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Preoff.Controllers
{
    /// <summary>
    /// 摄像头控制器
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Camera")]
    [EnableCors("AllowAllOrigins")]
    public class CameraController : BaseController
    {
        /// <summary>
        /// 摄像头仓库
        /// </summary>
        public readonly ICameraRepository _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public CameraController(ICameraRepository _db)
        {
            _repository = _db;
        }

        ///// <summary>
        ///// 添加摄像头[支持批量]
        ///// </summary>
        ///// <param name="_camera">摄像头类</param>
        ///// <returns></returns>
        //[HttpPost("addMul")]
        //public IActionResult Add([FromBody]List<CameraTable> _camera)
        //{
        //    try
        //    {
        //        int count=_repository.SaveList(_camera);
        //        return Json(new
        //        {
        //            count,
        //            state = "0",
        //            msg = "操作成功！"
        //        });
        //        //return Ok(_repository.SaveList(_camera));
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new
        //        {
        //            state = "-1",
        //            msg = "非法操作！"
        //        });
        //    }
        //}
        /// <summary>
        /// 添加摄像头返回摄像头id
        /// </summary>
        /// <param name="_camera">摄像头</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]CameraTable _camera)
        {
            try
            {
                //return Ok(_repository.SaveGetId(_camera));
                int id = _repository.SaveGetId(_camera);
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
        /// 更新摄像头[所有字段,支持批量]
        /// </summary>
        /// <param name="_camera">摄像头类</param>
        /// <returns></returns>
        [HttpPost("UpdateList")]
        public IActionResult UpdateList([FromBody]List<CameraTable> _camera)
        {
            try
            {
                //return Ok(_repository.UpdateList(_camera));
                int count = _repository.UpdateList(_camera);
                return Json(new
                {
                    count,
                    state = "0",
                    msg = "操作成功！"
                });
            }
            catch (Exception ex)
            {

                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 删除指定Id摄像头
        /// </summary>
        /// <param name="id">摄像头ID</param>
        /// <returns></returns>
        [HttpDelete("del/{id}")]
        public IActionResult Del(int id)
        {
            try
            {
                //return Ok(_repository.Delete(p => p.Id == id));
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
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 批量删除摄像头[根据摄像头ID集合批量删除]
        /// </summary>
        /// <param name="_cameraID">摄像头列表</param>
        /// <returns></returns>
        [HttpDelete("delids")]
        public IActionResult DelByIds([FromBody]List<int> _cameraID)
        {
            try
            {
                //return Ok(_repository.Delete(p => _cameraID.Contains(p.Id)));
                int count = _repository.Delete(p => _cameraID.Contains(p.Id));
                return Json(new
                {
                    count,
                    state = "0",
                    msg = "操作成功！"
                });
            }
            catch (Exception ex)
            {
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 批量删除摄像头[根据摄像头集合批量删除]
        /// </summary>
        /// <param name="_camera">摄像头列表</param>
        /// <returns></returns>
        [HttpDelete("batchdel")]
        public IActionResult Batchdel([FromBody]List<CameraTable> _camera)
        {
            try
            {
                //return Ok(_repository.DeleteList(_camera));
                int count = _repository.DeleteList(_camera);
                return Json(new
                {
                    count,
                    state = "0",
                    msg = "操作成功！"
                });
            }
            catch (Exception ex)
            {
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 根据摄像头ID查询摄像头
        /// </summary>
        /// <param name="id">摄像头ID</param>
        /// <returns></returns>
        [HttpGet("select/{id}")]
        public IActionResult Select(int id)
        {
            try
            {
                //return Ok(_repository.Get(p => p.Id == id));
                return Json(new
                {
                    table= _repository.Single(id),
                    state = "0",
                    msg = "操作成功！"
                });
            }
            catch (Exception ex)
            {
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="camera"></param>
        /// <returns>返回查询结果</returns>
        [HttpPost("selectCondition")]
        public IActionResult SelectCondition([FromBody]RequestCamera camera)
        {
            try
            {
                List<CameraTable> list = _repository.GetByCondition(camera);
                return Json(new
                {
                    table = list,
                    state = "0",
                    msg = "获取数据成功"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "获取数据出现错误",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// 查询所有摄像头
        /// </summary>
        /// <returns>返回所有摄像头</returns>
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
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        [HttpPost("Page")]
        public IActionResult SelectPage(int pageIndex, int pageSize, [FromBody]List<FilterStr> filter, string order, bool isAsc)
        {
            try
            {
                string _order = string.Empty;
                Expression<Func<CameraView, string>> orderby = null;
                Expression<Func<CameraView, int>> orderbyint = null;
                Expression<Func<CameraView, bool>> where = null;

                GetOrder(order, ref _order, ref orderby, ref orderbyint);
                if (filter != null && filter.Count > 0)
                {
                    string _filter = string.Empty;
                    foreach (FilterStr item in filter)
                    {
                        _filter += "p." + item.FieldName;
                        _filter = SwitchOper.SwitchOperation(_filter, item);
                        switch (item.Value.GetType().Name.ToString())
                        {
                            case "String":
                            case "string":
                                if (item.Operation == OperationStr.Like)
                                {
                                    _filter += item.Value;
                                    _filter += "\")";
                                }
                                else if (item.Operation == OperationStr.Equal || item.Operation == OperationStr.NotEqual)
                                {
                                    _filter += "\"";
                                    _filter += item.Value;
                                    _filter += "\"";
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        state = "-1",
                                        msg = "条件无效！"
                                    });
                                }
                                break;
                            case "Int32":
                            case "Int64":
                            case "Int":
                            case "Double":
                                if (item.Operation == OperationStr.Like)
                                {
                                    return Json(new
                                    {
                                        state = "-1",
                                        msg = "条件无效！"
                                    });
                                }
                                else
                                {
                                    _filter += item.Value;
                                }
                                break;
                            case "DateTime":
                                if (item.Operation == OperationStr.Like)
                                {
                                    return Json(new
                                    {
                                        state = "-1",
                                        msg = "条件无效！"
                                    });
                                }
                                else
                                {
                                    _filter += "DateTime.Parse(\"";
                                    _filter += item.Value;
                                    _filter += "\")";
                                }
                                break;
                            default:
                                break;
                        }
                        _filter += "&&";
                    }
                    _filter = _filter.Substring(0, _filter.Length - 2);
                    where = new Interpreter().ParseAsExpression<Func<CameraView, bool>>(_filter, "p");
                }

                if (orderbyint == null)
                {
                    return Json(new
                    {
                        table = _repository.Query<CameraView, string>(pageIndex, pageSize, where, orderby, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        table = _repository.Query<CameraView, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
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



        private static void GetOrder(string order, ref string _order, ref Expression<Func<CameraView, string>> orderby, ref Expression<Func<CameraView, int>> orderbyint)
        {
            if (order != null && order != string.Empty)
            {
                _order = "x." + order;
                try
                {
                    orderby = new Interpreter().ParseAsExpression<Func<CameraView, string>>(_order, "x");

                }
                catch (Exception ex)
                {
                    try
                    {
                        orderbyint = new Interpreter().ParseAsExpression<Func<CameraView, int>>(_order, "x");
                    }
                    catch (Exception e)
                    {
                        
                    }

                }

            }
        }
    }
}
