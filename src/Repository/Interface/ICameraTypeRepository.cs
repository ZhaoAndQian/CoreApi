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
    public interface ICameraTypeRepository : IRepository<CameraTypeTable>
    {
        CameraTypeView Single(int id);
    }
}
