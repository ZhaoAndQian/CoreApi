using Preoff.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Repository
{
    /// <summary>
    /// 区域仓储接口
    /// </summary>
    public interface IDivisionRepository : IRepository<DivisionTable>
    {
        List<DivisionTable> GetParent(string id);
    }
}
