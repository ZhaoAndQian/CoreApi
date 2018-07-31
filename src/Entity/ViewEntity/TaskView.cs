using System;

namespace Preoff.Entity
{
    public partial class TaskView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public int? TaskTypeTableId { get; set; }
        public string TaskTypeName { get; set; }
        public int? UserTableId { get; set; }
        public string ViewName { get; set; }
        public DateTime? PubTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TaskDesc { get; set; }
        public int? TaskStateTableId { get; set; }
        public string StateName { get; set; }
        public int? ExecTaskTableId { get; set; }
    }
}
