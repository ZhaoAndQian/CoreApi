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
    public interface IEventRepository : IRepository<EventTable>
    {
        EventView Single(int id);
        List<EventTable> GetJoinQuery(RequestEvent task);
    }
}
