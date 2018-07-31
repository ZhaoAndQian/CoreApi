using Preoff.Entity;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IAirLoadRepository : IRepository<AirLoadTable>
    {
        AirLoadView Single(int id);
    }
}
