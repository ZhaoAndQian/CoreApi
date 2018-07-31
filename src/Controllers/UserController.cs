using System;
using System.Collections.Generic;
using Preoff.Entity;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Preoff.Repository;
using System.Linq.Expressions;
using Preoff.Comm;
using DynamicExpresso;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Preoff.Entity.RequestEntity;

namespace Preoff.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("user")]
    [EnableCors("AllowAllOrigins")]
    public class UserController : BaseController
    {

        /// <summary>
        /// 用户仓库
        /// </summary>
        public readonly IUserRepository _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public UserController(IUserRepository _db)
        {
            _repository = _db;
        }


        ///// <summary>
        ///// 添加用户[支持批量]
        ///// </summary>
        ///// <param name="_user">用户类</param>
        ///// <returns></returns>
        //[HttpPost("addMul")]
        //public IActionResult Add([FromBody]List<UserTable> _user)
        //{
        //    try
        //    {
        //        //return Ok(_repository.SaveList(_user));
        //        int count = _repository.SaveList(_user);
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
        /// 添加用户返回用户id
        /// </summary>
        /// <param name="_user">用户</param>
        /// <returns></returns>
        [HttpPost("addone")]
        public IActionResult Add([FromBody]UserTable _user)
        {
            try
            {
                if (_user == null)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "请输入用户！"
                    });
                }
                if (_repository.IsExist(p => p.LoginName == _user.LoginName))
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "帐号已存在！"
                    });
                }
                if (_repository.IsExist(p => p.ViewName == _user.ViewName))
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "昵称已存在！"
                    });
                }
                _user.RegTime = DateTime.Now;
                _user.LoginPwd = Pwd.Ecoding(_user.LoginPwd);
                int id = _repository.SaveGetId(_user);
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
        /// 更新用户[所有字段,支持批量]
        /// </summary>
        /// <param name="_user">用户类</param>
        /// <returns></returns>
        [HttpPost("UpdateList")]
        public IActionResult UpdateList([FromBody]List<UserTable> _user)
        {
            try
            {
                //return Ok(_repository.UpdateList(_user));
                int count = _repository.UpdateList(_user);
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
        /// 删除指定Id用户
        /// </summary>
        /// <param name="id">用户ID</param>
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

                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
        /// <summary>
        /// 批量删除用户[根据用户ID集合批量删除]
        /// </summary>
        /// <param name="_userID">用户列表</param>
        /// <returns></returns>
        [HttpDelete("delids")]
        public IActionResult DelByIds([FromBody]List<int> _userID)
        {
            try
            {
                //return Ok(_repository.Delete(p=> _userID.Contains(p.Id)));
                int count = _repository.Delete(p => _userID.Contains(p.Id));
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
        /// 批量删除用户[根据用户集合批量删除]
        /// </summary>
        /// <param name="_user">用户列表</param>
        /// <returns></returns>
        [HttpDelete("batchdel")]
        public IActionResult Batchdel([FromBody]List<UserTable> _user)
        {
            try
            {
                //return Ok(_repository.DeleteList(_user));
                int count = _repository.DeleteList(_user);
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
        /// 根据用户ID查询用户
        /// </summary>
        /// <param name="id">用户ID</param>
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
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns>返回查询结果</returns>
        [HttpPost("selectCondition")]
        public IActionResult SelectCondition([FromBody]RequestUser user)
        {
            try
            {
                List<UserTable> list = _repository.GetByCondition(user);
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
        /// 查询所有用户
        /// </summary>
        /// <returns>返回所有用户</returns>
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
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }

        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="pwd">原始密码</param>
        /// <param name="newpwd">新密码</param>
        /// <param name="isAdmin">是否超级管理员</param>
        /// <returns></returns>
        [HttpPut("changpwd")]
        public IActionResult ChangePwd(int id,string pwd,string newpwd,bool isAdmin=false)
        {
            if (isAdmin)
            {
                UserTable _ut = _repository.Get(p => p.Id == id);
                _ut.LoginPwd = Pwd.Ecoding(newpwd);
                if (_repository.Update(_ut))
                {
                    return Json(new
                    {
                        state = "0",
                        msg = "密码修改成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "密码修改失败！"
                    });

                }
            }
            else
            {
                UserTable _ut = _repository.Get(p => p.Id == id && p.LoginPwd == Pwd.Ecoding(pwd));
                if (_ut is null)
                {
                    return Json(new
                    {
                        state = "-1",
                        msg = "原始密码错误！"
                    });
                }
                else
                {
                    _ut.LoginPwd = Pwd.Ecoding(newpwd);
                    if (_repository.Update(_ut))
                    {
                        return Json(new
                        {
                            state = "0",
                            msg = "密码修改成功！"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            state = "-1",
                            msg = "密码修改失败！"
                        });

                    }
                }
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
                Expression<Func<UserView, string>> orderby = null;
                Expression<Func<UserView, int>> orderbyint = null;
                Expression<Func<UserView, bool>> where = null;

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
                    where = new Interpreter().ParseAsExpression<Func<UserView, bool>>(_filter, "p");
                }

                if (orderbyint == null)
                {
                    return Json(new
                    {
                        table = _repository.Query<UserView, string>(pageIndex, pageSize, where, orderby, null, isAsc),
                        state = "0",
                        msg = "操作成功！"
                    });
                }
                else
                {
                    return Json(new
                    {
                        table = _repository.Query<UserView, int>(pageIndex, pageSize, where, orderbyint, null, isAsc),
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
        private static void GetOrder(string order, ref string _order, ref Expression<Func<UserView, string>> orderby, ref Expression<Func<UserView, int>> orderbyint)
        {
            if (order != null && order != string.Empty)
            {
                _order = "x." + order;
                try
                {
                    orderby = new Interpreter().ParseAsExpression<Func<UserView, string>>(_order, "x");

                }
                catch (Exception ex)
                {
                    try
                    {
                        orderbyint = new Interpreter().ParseAsExpression<Func<UserView, int>>(_order, "x");
                    }
                    catch (Exception e)
                    {
                        
                    }

                }

            }
        }
    }

}

