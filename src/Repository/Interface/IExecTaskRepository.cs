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
    public interface IExecTaskRepository : IRepository<ExecTaskTable>
    {
        ExecTaskView Single(int id);
        bool UpdateStatus(int execTaskId, int statusId, DateTime endTime);
        List<ExecTaskTable> GetJoinQuery(RequestExecTask task);
    }
}
