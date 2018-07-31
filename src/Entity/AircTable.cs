using System;

namespace Preoff.Entity
{
    public partial class AircTable
    {
        public int Id { get; set; }
        public string SerialNum { get; set; }
        public int? UnitTableId { get; set; }
        public int? AircTypeTableId { get; set; }
        public int? AirLoadTableId { get; set; }
        public int? AirFacTableId { get; set; }
        public string UsedDesc { get; set; }
        public string AirDesc { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
