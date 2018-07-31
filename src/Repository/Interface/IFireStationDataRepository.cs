using Preoff.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IFireStationDataRepository : IRepository<FireStationDataTable>
    {
        IQueryable<ReturnEntity> GetEntity(string date,string hour);
    }
}
