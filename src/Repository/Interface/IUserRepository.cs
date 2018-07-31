using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<UserTable>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="_user"></param>
        ///// <returns></returns>
        //int UpdateList(List<UserTable> _user);
        UserView Single(int id);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="_user"></param>
        ///// <returns></returns>
        //int SaveList(List<UserTable> _user);
        List<UserTable> GetByCondition(RequestUser user);
    }
}
