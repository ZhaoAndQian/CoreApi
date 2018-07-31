using System;
using System.Collections.Generic;
using System.Text;

namespace Preoff.Entity.RequestEntity
{
    public class RequestAirc : BaseRequest
    {
        public string SerialNum { get; set; }
        public int? AircTypeTableId { get; set; }
        public int? AirFacTableId { get; set; }
    }
}
