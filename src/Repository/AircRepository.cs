using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System.Collections.Generic;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class AircRepository : RepositoryBase<AircTable>, IAircRepository
    {
        PreoffContext _dbcontext;
        public AircRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<AircTable> GetByCondition(RequestAirc airc)
        {
            List<AircTable> list = _dbcontext.AircTable.WhereIf(!string.IsNullOrWhiteSpace(airc.SerialNum), t => t.SerialNum.Contains(airc.SerialNum))
                .WhereIf(airc.AircTypeTableId.HasValue, t => t.AircTypeTableId == airc.AircTypeTableId)
                .WhereIf(airc.AirFacTableId.HasValue, t => t.AirFacTableId == airc.AirFacTableId).ToList();
            return list;
        }

        public AircView Single(int id)
        {
            return _dbcontext.AircView.FirstOrDefault(p => p.Id == id);
        }
    }
}
