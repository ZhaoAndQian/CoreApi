using Preoff.Entity;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class FireStationDataRepository : RepositoryBase<FireStationDataTable>, IFireStationDataRepository
    {
        PreoffContext _dbcontext;
        public FireStationDataRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

 
        public IQueryable<ReturnEntity> GetEntity(string date, string hour)
        {
            IQueryable<ReturnEntity> r = _dbcontext.FireStationData.Where(p => p.CatDate == date && p.CatHour == hour).Select(x => new ReturnEntity {code=x.Code,firelevel=x.Firelevel});
            
            return r;
        }


    }
}
