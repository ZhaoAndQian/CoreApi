namespace Preoff.Entity
{
    public partial class CameraView
    {
        public int Id { get; set; }
        public int? UnitTableId { get; set; }
        public string UnitName { get; set; }
        public int? CameraTypeTableId { get; set; }
        public string CameraTypeName { get; set; }
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
