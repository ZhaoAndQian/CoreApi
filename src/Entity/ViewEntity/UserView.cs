using System;

namespace Preoff.Entity
{
    public partial class UserView
    {
        public int Id { get; set; }
        public int? UnitTableId { get; set; }
        public string UnitName { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string RealName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ViewName { get; set; }
        public DateTime? RegTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? LoginCount { get; set; }
    }
}
