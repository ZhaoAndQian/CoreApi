
namespace Preoff.Entity
{
    public partial class TaskUserTable
    {
        public int Id { get; set; }
        public int? TaskTableId { get; set; }
        public int? UserTableId { get; set; }
    }
}
