using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Preoff.Repository;
using System;

namespace Preoff.Controllers
{
    /// <summary>
    /// 行政区控制器
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Division")]
    [EnableCors("AllowAllOrigins")]
    public class DivisionController : BaseController
    {
        /// <summary>
        /// 行政区仓库
        /// </summary>
        public readonly IDivisionRepository _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public DivisionController(IDivisionRepository _db)
        {
            _repository = _db;
        }
        /// <summary>
        /// 获取省
        /// </summary>
        /// <returns>返回省</returns>
        [HttpGet("selectfirst")]
        public IActionResult Seletfirst()
        {
            try
            {
                //return Ok(_repository.Get(p => p.PId == "000000000000"));
                return Json(new
                {
                    table = _repository.Get(p => p.Id == "000000000000"),
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
        /// 根据父节点ID查询下一级子节点
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <returns></returns>
        [HttpGet("selectsub/{id}")]
        public IActionResult Select(string id)
        {
            try
            {
                //return Ok(_repository.LoadAll(p => p.PId == id));
                return Json(new
                {
                    table = _repository.LoadAll(p => p.PId == id),
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
        /// 根据子节点ID查询父节点
        /// </summary>
        /// <returns>父节点ID</returns>
        [HttpGet("selectparent/{id}")]
        public IActionResult SelectAll(string id)
        {
            try
            {
                return Ok(_repository.GetParent(id));
            }
            catch (Exception ex)
            {
                return Json(new {
                    state = "-1",
                    msg = "非法操作！"
                });
            }

        }

        ///// <summary>
        ///// 分页
        ///// </summary>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">每页数据条数</param>
        ///// <param name="filter">过滤条件</param>
        ///// <param name="order">排序字段</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <returns></returns>
        //[HttpPost("Page")]
        //public IActionResult SelectPage(int pageIndex, int pageSize, [FromBody]List<FilterStr> filter, string order, bool isAsc)
        //{
        //    try
        //    {
        //        string _order = string.Empty;
        //        Expression<Func<UserTable, string>> orderby = null;
        //        Expression<Func<UserTable, int>> orderbyint = null;
        //        Expression<Func<UserTable, bool>> where = null;

        //        GetOrder(order, ref _order, ref orderby, ref orderbyint);
        //        if (filter != null && filter.Count > 0)
        //        {
        //            string _filter = string.Empty;
        //            foreach (FilterStr item in filter)
        //            {
        //                _filter += "p." + item.FieldName;
        //                _filter = SwitchOper.SwitchOperation(_filter, item);
        //                switch (item.Value.GetType().Name.ToString())
        //                {
        //                    case "String":
        //                    case "string":
        //                        if (item.Operation == OperationStr.Like)
        //                        {
        //                            _filter += item.Value;
        //                            _filter += "\")";
        //                        }
        //                        else if (item.Operation == OperationStr.Equal || item.Operation == OperationStr.NotEqual)
        //                        {
        //                            _filter += "\"";
        //                            _filter += item.Value;
        //                            _filter += "\"";
        //                        }
        //                        else
        //                        {
        //                            return Json(new
        //                            {
        //                                state = "-1",
        //                                msg = "条件无效！"
        //                            });
        //                        }
        //                        break;
        //                    case "Int32":
        //                    case "Int64":
        //                    case "Int":
        //                    case "Double":
        //                        if (item.Operation == OperationStr.Like)
        //                        {
        //                            return Json(new
        //                            {
        //                                state = "-1",
        //                                msg = "条件无效！"
        //                            });
        //                        }
        //                        else
        //                        {
        //                            _filter += item.Value;
        //                        }
        //                        break;
        //                    case "DateTime":
        //                        if (item.Operation == OperationStr.Like)
        //                        {
        //                            return Json(new
        //                            {
        //                                state = "-1",
        //                                msg = "条件无效！"
        //                            });
        //                        }
        //                        else
        //                        {
        //                            _filter += "DateTime.Parse(\"";
        //                            _filter += item.Value;
        //                            _filter += "\")";
        //                        }
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                _filter += "&&";
        //            }
        //            _filter = _filter.Substring(0, _filter.Length - 2);
        //            where = new Interpreter().ParseAsExpression<Func<UserTable, bool>>(_filter, "p");
        //        }

        //        if (orderbyint == null)
        //        {
        //            return Json(new
        //            {
        //                table = _repository.Query<UserTable, string>(pageIndex, pageSize, where, orderby, null, isAsc),
        //                state = "0",
        //                msg = "操作成功！"
        //            });
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                table = _repository.Query<UserTable, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
        //                state = "0",
        //                msg = "操作成功！"
        //            });
        //        }
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


        //private static void GetOrder(string order, ref string _order, ref Expression<Func<UserTable, string>> orderby, ref Expression<Func<UserTable, int>> orderbyint)
        //{
        //    if (order != null && order != string.Empty)
        //    {
        //        _order = "x." + order;
        //        try
        //        {
        //            orderby = new Interpreter().ParseAsExpression<Func<UserTable, string>>(_order, "x");

        //        }
        //        catch (Exception ex)
        //        {
        //            try
        //            {

        //            }
        //            catch (Exception e)
        //            {
        //                orderbyint = new Interpreter().ParseAsExpression<Func<UserTable, int>>(_order, "x");
        //            }

        //        }

        //    }
        //}
    }
}
