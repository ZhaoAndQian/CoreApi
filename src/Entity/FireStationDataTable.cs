
namespace Preoff.Entity
{
    public partial class FireStationDataTable
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CatDate { get; set; }
        public string CatHour { get; set; }
        public string DateType { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Rain { get; set; }
        public double? WindSpeed { get; set; }
        public double? Winddirect { get; set; }
        public int? Firelevel { get; set; }
    }
}
