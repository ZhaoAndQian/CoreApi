using Preoff.Entity;
using System.Data;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class PlaceRepository : RepositoryBase<PlaceTable>, IPlaceRepository
    {
        PreoffContext _dbcontext;
        public PlaceRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

 
        public IQueryable<string> GetProvince()
        {
            IQueryable<string> r =_dbcontext.PlaceTable.Select(x=>x.Province).Distinct();
            return r;
        }

        public IQueryable<string> GetPlaceType()
        {
            IQueryable<string> r = _dbcontext.PlaceTable.Select(x => x.PlaceType).Distinct();
            return r;
        }
        public IQueryable<PlaceTable> GetPlace(string province, string placetype)
        {
            IQueryable<PlaceTable> r = _dbcontext.PlaceTable.Where(p=>p.Province==province && p.PlaceType==placetype);
            return r;
        }
    }
}
