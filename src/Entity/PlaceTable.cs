using System;
using System.Collections.Generic;
using System.Text;

namespace Preoff.Entity
{
    public partial class PlaceTable
    {
        public int Id { get; set; }

        public string PlaceName { get; set; }
        public string Province { get; set; }
        public string PlaceType { get; set; }
        public double? MaxX { get; set; }
        public double? MaxY { get; set; }
        public double? MinX { get; set; }
        public double? MinY { get; set; }
        public string LayerID { get; set; }

    }
}
