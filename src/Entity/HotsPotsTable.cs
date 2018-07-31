using System;

namespace Preoff.Entity
{
    public partial class HotsPotsTable
    {
        public int Id { get; set; }
        public string HotsPotsCode { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string CountyName { get; set; }
        public string FeedbackSituation { get; set; }
        public string Continuously { get; set; }
        public int? PixelsNumber { get; set; }
        public string LandType { get; set; }
        public string SatelLite { get; set; }
        public string Smoke { get; set; }
        public DateTime? IgnitingTime { get; set; }
        public DateTime? AttackTime { get; set; }
        public string CauseOfFire { get; set; }
        public string BriefIntroduction { get; set; }
        public double? FireArea { get; set; }
        public double? FireForestryArea { get; set; }
        public double? VictimForestryArea { get; set; }
        public string OtherLosses { get; set; }
        public string ReportUnit { get; set; }
        public string ReportUser { get; set; }
        public string MonitorUnit { get; set; }
        public string MonitorUser { get; set; }
        public double? XPos { get; set; }
        public double? YPos { get; set; }
        public string PicPath { get; set; }
        public string ReportFile { get; set; }
        public string SystemCode { get; set; }
    }
}
