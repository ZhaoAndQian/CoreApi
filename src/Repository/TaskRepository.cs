using Preoff.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Preoff.Entity.RequestEntity;

namespace Preoff.Repository
{
    public sealed class TaskRepository : RepositoryBase<TaskTable>, ITaskRepository
    {
        PreoffContext _dbcontext;
        public TaskRepository(PreoffContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public TaskView Single(int id)
        {
            return _dbcontext.TaskView.FirstOrDefault(p => p.Id == id);
        }

        public List<TaskTable> GetJoinQuery(RequestTask task)
        {
            List<TaskTable> list= _dbcontext.TaskTable.Include(t => t.ListExec)
                .WhereIf(!string.IsNullOrWhiteSpace(task.TaskName),t=>t.TaskName.Contains(task.TaskName))
                .WhereIf(task.TaskTypeTableId.HasValue,t=>t.TaskTypeTableId==task.TaskTypeTableId)
                .OrderByDescending(t=>t.PubTime).ToList();
            return list;
        }
    }
}
