
namespace Preoff.Entity
{
    public partial class UnitView
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string DivisionTableId { get; set; }
        public string UnitAddr { get; set; }
        public string UnitPhone { get; set; }
        public string UnitDesc { get; set; }
        public int? StreamVideoServerTableId { get; set; }
        public string ServerName { get; set; }
    }
}
