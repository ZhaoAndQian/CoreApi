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
    public interface ITaskRepository : IRepository<TaskTable>
    {
        TaskView Single(int id);
        List<TaskTable> GetJoinQuery(RequestTask task);
    }
}
