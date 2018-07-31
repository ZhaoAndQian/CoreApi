using Preoff.Entity;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class UnitRepository : RepositoryBase<UnitTable>, IUnitRepository
    {
        PreoffContext _dbcontext;
        public UnitRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public UnitView Single(int id)
        {
            return _dbcontext.UnitView.FirstOrDefault(p => p.Id == id);
        }
    }
}
