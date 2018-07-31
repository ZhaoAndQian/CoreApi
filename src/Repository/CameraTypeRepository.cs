using Preoff.Entity;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class CameraTypeRepository : RepositoryBase<CameraTypeTable>, ICameraTypeRepository
    {
        PreoffContext _dbcontext;
        public CameraTypeRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public CameraTypeView Single(int id)
        {
            return _dbcontext.CameraTypeView.FirstOrDefault(p => p.Id == id);
        }
    }
}
