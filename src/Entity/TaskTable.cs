using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Preoff.Entity
{
    public partial class TaskTable
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int? TaskTypeTableId { get; set; }
        public int? UserTableId { get; set; }
        public DateTime? PubTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TaskDesc { get; set; }
        public virtual List<ExecTaskTable> ListExec{get;set;}
    }

}
