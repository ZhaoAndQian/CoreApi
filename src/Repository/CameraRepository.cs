using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System.Collections.Generic;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class CameraRepository : RepositoryBase<CameraTable>, ICameraRepository
    {
        PreoffContext _dbcontext;
        public CameraRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public CameraView Single(int id)
        {
            return _dbcontext.CameraView.FirstOrDefault(p => p.Id == id);
        }

        public List<CameraTable> GetByCondition(RequestCamera camera)
        {
            List<CameraTable> list=_dbcontext.CameraTable.WhereIf(!string.IsNullOrWhiteSpace(camera.IpAddr), t => t.IpAddr.Contains(camera.IpAddr))
                .WhereIf(!string.IsNullOrWhiteSpace(camera.CameraName), t => t.CameraName.Contains(camera.CameraName))
                .WhereIf(camera.CameraTypeTableId.HasValue, t => t.CameraTypeTableId == camera.CameraTypeTableId).ToList();
            return list;
        }
    }
}
