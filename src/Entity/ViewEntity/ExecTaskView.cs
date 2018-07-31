using System;

namespace Preoff.Entity
{
    public partial class ExecTaskView
    {
        public int Id { get; set; }
        public int? TaskTableId { get; set; }
        public string TaskName { get; set; }
        public int? UserTableId { get; set; }
        public string ViewName { get; set; }
        public int? AircTableId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TaskStateTableId { get; set; }
        public string StateName { get; set; }
    }
}
