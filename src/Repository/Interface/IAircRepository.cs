using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System.Collections.Generic;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IAircRepository : IRepository<AircTable>
    {
        AircView Single(int id);

        List<AircTable> GetByCondition(RequestAirc airc);
    }
}
