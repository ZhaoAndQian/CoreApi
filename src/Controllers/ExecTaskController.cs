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
    /// 执行任务控制器
    /// </summary>
    //[Authorize]
    [Produces("application/json")]
    [Route("ExecTask")]
    [EnableCors("AllowAllOrigins")]
    public class ExecTaskController : BaseController
    {
        /// <summary>
        /// 执行任务仓库
        /// </summary>
        public readonly IExecTaskRepository _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public ExecTaskController(IExecTaskRepository _db)
        {
            _repository = _db;
        }

        ///// <summary>
        ///// 添加执行任务[支持批量]
        ///// </summary>
        ///// <param name="_execTask">执行任务类</param>
        ///// <returns></returns>
        //[HttpPost("addMul")]
        //public IActionResult Add([FromBody]List<ExecTaskTable> _execTask)
        //{
        //    try
        //    {
        //        int count=_repository.SaveList(_execTask);
        //        return Json(new
        //        {
        //            count,
        //            state = "0",
        //            msg = "操作成功！"
        //        });
        //        //return Ok(_repository.SaveList(_execTask));
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
        /// 添加执行任务返回执行任务id
        /// </summary>
        /// <param name="_execTask">执行任务</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]ExecTaskTable _execTask)
        {
            try
            {
                _execTask.TaskStateTableId = 1;
                int id = _repository.SaveGetId(_execTask);
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
        /// 更新执行任务[所有字段,支持批量]
        /// </summary>
        /// <param name="_execTask">执行任务类</param>
        /// <returns></returns>
        [HttpPost("UpdateList")]
        public IActionResult UpdateList([FromBody]List<ExecTaskTable> _execTask)
        {
            try
            {
                //return Ok(_repository.UpdateList(_execTask));
                int count = _repository.UpdateList(_execTask);
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
        /// 更新执行任务状态
        /// </summary>
        /// <param name="execTaskId">执行任务id</param>
        /// <param name="statusId">执行任务状态</param>
        /// <param name="endTime">执行结束时间</param>
        /// <returns></returns>
        [HttpPost("UpdateStatus")]
        public IActionResult UpdateStatus(int execTaskId,int statusId,DateTime endTime)
        {
            try
            {
                if (_repository.UpdateStatus(execTaskId, statusId, endTime))
                {
                    return Json(new
                    {
                        state = "0",
                        msg = "更新成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "更新失败！"
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


        /// <summary>
        /// 删除指定Id执行任务
        /// </summary>
        /// <param name="id">执行任务ID</param>
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
        /// 批量删除执行任务[根据执行任务ID集合批量删除]
        /// </summary>
        /// <param name="_execTaskID">执行任务列表</param>
        /// <returns></returns>
        [HttpDelete("delids")]
        public IActionResult DelByIds([FromBody]List<int> _execTaskID)
        {
            try
            {
                //return Ok(_repository.Delete(p => _execTaskID.Contains(p.Id)));
                int count = _repository.Delete(p => _execTaskID.Contains(p.Id));
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
        /// 批量删除执行任务[根据执行任务集合批量删除]
        /// </summary>
        /// <param name="_execTask">执行任务列表</param>
        /// <returns></returns>
        [HttpDelete("batchdel")]
        public IActionResult Batchdel([FromBody]List<ExecTaskTable> _execTask)
        {
            try
            {
                //return Ok(_repository.DeleteList(_execTask));
                int count = _repository.DeleteList(_execTask);
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
        /// 根据执行任务ID查询执行任务
        /// </summary>
        /// <param name="id">执行任务ID</param>
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
        /// 查询所有执行任务
        /// </summary>
        /// <returns>返回所有执行任务</returns>
        [HttpGet("selectall")]
        public IActionResult SelectAll()
        {
            try
            {
                //return Ok(_repository.LoadListAll());
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
        /// 条件查询
        /// </summary>
        /// <param name="task"></param>
        /// <returns>返回关联查询数据</returns>
        [HttpPost("selectCondition")]
        public IActionResult SelectCondition([FromBody]RequestExecTask task)
        {
            try
            {
                List<ExecTaskTable> list = _repository.GetJoinQuery(task);
                return Json(new
                {
                    table = list,
                    state = "0",
                    msg = "获取数据成功"
                }, this.GetJsonSetting());
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "获取数据出错",
                    error = ex.Message
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
                Expression<Func<ExecTaskView, string>> orderby = null;
                Expression<Func<ExecTaskView, int>> orderbyint = null;
                Expression<Func<ExecTaskView, bool>> where = null;

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
                    where = new Interpreter().ParseAsExpression<Func<ExecTaskView, bool>>(_filter, "p");
                }

                if (orderbyint == null)
                {
                    return Json(new
                    {
                        table = _repository.Query<ExecTaskView, string>(pageIndex, pageSize, where, orderby, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        table = _repository.Query<ExecTaskView, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
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


        private static void GetOrder(string order, ref string _order, ref Expression<Func<ExecTaskView, string>> orderby, ref Expression<Func<ExecTaskView, int>> orderbyint)
        {
            if (order != null && order != string.Empty)
            {
                _order = "x." + order;
                try
                {
                    orderby = new Interpreter().ParseAsExpression<Func<ExecTaskView, string>>(_order, "x");

                }
                catch (Exception ex)
                {
                    try
                    {
                        orderbyint = new Interpreter().ParseAsExpression<Func<ExecTaskView, int>>(_order, "x");
                    }
                    catch (Exception e)
                    {
                        
                    }

                }

            }
        }
    }
}
