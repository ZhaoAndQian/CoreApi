namespace Preoff.Entity
{
    public partial class RolePermissionTable
    {
        public int Id { get; set; }
        public int? RoleTableId { get; set; }
        public int? PermissonTableId { get; set; }
    }
}
