
namespace Preoff.Entity
{
    public partial class UserRoleTable
    {
        public int Id { get; set; }
        public int? UserTableId { get; set; }
        public int? RoleTableId { get; set; }
    }
}
