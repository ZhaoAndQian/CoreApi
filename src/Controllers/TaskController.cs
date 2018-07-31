using DynamicExpresso;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Preoff.Comm;
using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using Preoff.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Preoff.Controllers
{
    /// <summary>
    /// 任务控制器
    /// </summary>
    //[Authorize]
    [Produces("application/json")]
    [Route("Task")]
    [EnableCors("AllowAllOrigins")]
    public class TaskController : BaseController
    {
        /// <summary>
        /// 任务仓库
        /// </summary>
        public readonly ITaskRepository _repository;
        /// <summary>
        /// 事件
        /// </summary>
        public readonly IEventRepository _eventRepository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        /// <param name="eventRepository">事件仓储</param>
        public TaskController(ITaskRepository _db, IEventRepository eventRepository)
        {
            _repository = _db;
            _eventRepository = eventRepository;
        }

        ///// <summary>
        ///// 添加任务[支持批量]
        ///// </summary>
        ///// <param name="_task">任务类</param>
        ///// <returns></returns>
        //[HttpPost("addMul")]
        //public IActionResult Add([FromBody]List<TaskTable> _task)
        //{
        //    try
        //    {
        //        //return Ok(_repository.SaveList(_task));
        //        int count = _repository.SaveList(_task);
        //        return Json(new
        //        {
        //            count,
        //            state = "0",
        //            msg = "操作成功！"
        //        });
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
        /// 添加任务返回任务id
        /// </summary>
        /// <param name="_task">任务</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]TaskTable _task)
        {
            try
            {
                //return Ok(_repository.SaveGetId(_task));
                int id = _repository.SaveGetId(_task);
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
        /// 更新任务[所有字段,支持批量]
        /// </summary>
        /// <param name="_task">任务类</param>
        /// <returns></returns>
        [HttpPost("UpdateList")]
        public IActionResult UpdateList([FromBody]List<TaskTable> _task)
        {
            try
            {
                //return Ok(_repository.UpdateList(_task));
                int count = _repository.UpdateList(_task);
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
        /// 删除指定Id任务
        /// </summary>
        /// <param name="id">任务ID</param>
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
        /// 批量删除任务[根据任务ID集合批量删除]
        /// </summary>
        /// <param name="_taskID">任务列表</param>
        /// <returns></returns>
        [HttpDelete("delids")]
        public IActionResult DelByIds([FromBody]List<int> _taskID)
        {
            try
            {
                //return Ok(_repository.Delete(p => _taskID.Contains(p.Id)));
                int count = _repository.Delete(p => _taskID.Contains(p.Id));
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
        /// 批量删除任务[根据任务集合批量删除]
        /// </summary>
        /// <param name="_task">任务列表</param>
        /// <returns></returns>
        [HttpDelete("batchdel")]
        public IActionResult Batchdel([FromBody]List<TaskTable> _task)
        {
            try
            {
                //return Ok(_repository.DeleteList(_task));
                int count = _repository.DeleteList(_task);
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
        /// 根据任务ID查询任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        [HttpGet("select/{id}")]
        public IActionResult Select(int id)
        {
            try
            {                
                return Json(new
                {
                    table = _repository.Single(id),
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
        /// <param name="task"></param>
        /// <returns>返回关联查询数据</returns>
        [HttpPost("selectCondition")]
        public IActionResult SelectCondition([FromBody]RequestTask task)
        {
            try
            {
                List<TaskTable> list = _repository.GetJoinQuery(task);
                foreach (var taskTable in list)
                {
                    if (taskTable.ListExec.Count>0)
                    {
                        foreach (var execTable in taskTable.ListExec)
                        {
                            execTable.ListEvent = _eventRepository.LoadAll(t => t.ExecTaskTableId.Value == execTable.Id).ToList();
                        }
                    }
                }
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
                    msg = "非法操作！",
                    error =ex.Message
                });
            }
        }

        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns>返回所有任务</returns>
        [HttpGet("selectall")]
        public IActionResult SelectAll()
        {
            try
            {
                List<TaskTable> list = _repository.LoadListAll();
                return Json(new
                {
                    table= list,
                    state = "0",
                    msg = "获取数据成功"
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
                Expression<Func<TaskTable, string>> orderby = null;
                Expression<Func<TaskTable, int>> orderbyint = null;
                Expression<Func<TaskTable, bool>> where = null;

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
                    where = new Interpreter().ParseAsExpression<Func<TaskTable, bool>>(_filter, "p");
                }

                if (orderbyint == null)
                {
                    return Json(new
                    {
                        table = _repository.Query<TaskTable, string>(pageIndex, pageSize, where, orderby, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        table = _repository.Query<TaskTable, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
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


        private static void GetOrder(string order, ref string _order, ref Expression<Func<TaskTable, string>> orderby, ref Expression<Func<TaskTable, int>> orderbyint)
        {
            if (order != null && order != string.Empty)
            {
                _order = "x." + order;
                try
                {
                    orderby = new Interpreter().ParseAsExpression<Func<TaskTable, string>>(_order, "x");

                }
                catch (Exception ex)
                {
                    try
                    {
                        orderbyint = new Interpreter().ParseAsExpression<Func<TaskTable, int>>(_order, "x");
                    }
                    catch (Exception e)
                    {
                        
                    }

                }

            }
        }

        private static void GetOrder(string order, ref string _order, ref Expression<Func<TaskView, string>> orderby, ref Expression<Func<TaskView, int>> orderbyint)
        {
            if (order != null && order != string.Empty)
            {
                _order = "x." + order;
                try
                {
                    orderby = new Interpreter().ParseAsExpression<Func<TaskView, string>>(_order, "x");

                }
                catch (Exception ex)
                {
                    try
                    {
                        orderbyint = new Interpreter().ParseAsExpression<Func<TaskView, int>>(_order, "x");
                    }
                    catch (Exception e)
                    {

                    }

                }

            }
        }
        /// <summary>
        /// 根据用户id返回对应任务，id为-1返回所有任务
        /// </summary>
        /// <param name="userid">接收任务的用户id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        [HttpPost("GetTaskFromUserID")]
        public IActionResult GetTask(int userid,int pageIndex, int pageSize, [FromBody]List<FilterStr> filter, string order, bool isAsc)
        { 
            if (userid!=-1)
            {
                if (filter is null)
                {
                    filter = new List<FilterStr>();
                }
                FilterStr _f = new FilterStr
                {
                    FieldName = "UserId",
                    Operation = OperationStr.Equal,
                    Value = userid
                };
                filter.Add(_f);
            }
            try
            {
                string _order = string.Empty;
                Expression<Func<TaskView, string>> orderby = null;
                Expression<Func<TaskView, int>> orderbyint = null;
                Expression<Func<TaskView, bool>> where = null;

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
                    where = new Interpreter().ParseAsExpression<Func<TaskView, bool>>(_filter, "p");
                }

                if (orderbyint == null)
                {
                    return Json(new
                    {
                        table = _repository.Query<TaskView, string>(pageIndex, pageSize, where, orderby, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        table = _repository.Query<TaskView, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
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

            //return Json(new
            //{
            //    table = _repository.GetTask(id),
            //    state = "0",
            //    msg = "操作成功!"
            //});
            //return Ok();
        }
    }
}
