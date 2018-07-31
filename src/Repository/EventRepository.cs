using Microsoft.EntityFrameworkCore;
using Preoff.Entity;
using Preoff.Entity.RequestEntity;
using System.Collections.Generic;
using System.Linq;

namespace Preoff.Repository
{
    public sealed class EventRepository : RepositoryBase<EventTable>, IEventRepository
    {
        PreoffContext _dbcontext;
        public EventRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public EventView Single(int id)
        {
            return _dbcontext.EventView.FirstOrDefault(p => p.Id == id);
        }

        public List<EventTable> GetJoinQuery(RequestEvent task)
        {
            List<EventTable> list = _dbcontext.EventTable
                .WhereIf(task.EventTypeTableId.HasValue, t => t.EventTypeTableId == task.EventTypeTableId)
                .WhereIf(task.ExecTaskTableId.HasValue, t => t.ExecTaskTableId == task.ExecTaskTableId)
                .OrderByDescending(t => t.EventTime).ToList();
            return list;
        }
    }
}
