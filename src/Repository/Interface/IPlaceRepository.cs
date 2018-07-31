using Preoff.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IPlaceRepository : IRepository<PlaceTable>
    {
        IQueryable<string> GetProvince();
        IQueryable<string> GetPlaceType();

        IQueryable<PlaceTable> GetPlace(string province, string placetype);
    }
}
