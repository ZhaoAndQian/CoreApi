using Microsoft.EntityFrameworkCore;
using Preoff.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class DivisionRepository : RepositoryBase<DivisionTable>, IDivisionRepository
    {
        private PreoffContext _dbContext;
        public DivisionRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }


        public List<DivisionTable> GetParent(string id)
        {
            var Id = new SqlParameter("Id", id);
            var iQueryTable = _dbContext.Set<DivisionTable>().FromSql("EXECUTE dbo.GetFullAddr @Id",Id);
            return iQueryTable.ToList();
        }
    }
}
