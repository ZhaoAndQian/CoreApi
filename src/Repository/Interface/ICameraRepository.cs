using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System.Collections.Generic;

namespace Preoff.Repository
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface ICameraRepository : IRepository<CameraTable>
    {
        CameraView Single(int id);
        List<CameraTable> GetByCondition(RequestCamera camera);
    }
}
