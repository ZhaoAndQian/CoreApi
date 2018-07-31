
namespace Preoff.Entity
{
    public partial class CameraTable
    {
        public int Id { get; set; }
        public int? UnitTableId { get; set; }
        public int? CameraTypeTableId { get; set; }
        public string IpAddr { get; set; }
        public string CameraPort { get; set; }
        public string CameraName { get; set; }
        public string CameraPwd { get; set; }
        public double? CameraX { get; set; }
        public double? CameraY { get; set; }
        public double? CameraZ { get; set; }
        public string CameraAddr { get; set; }
    }
}
