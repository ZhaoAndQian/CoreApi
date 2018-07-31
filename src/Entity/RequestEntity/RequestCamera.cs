using System;
using System.Collections.Generic;
using System.Text;

namespace Preoff.Entity.RequestEntity
{
    public class RequestCamera : BaseRequest
    {
        public int? CameraTypeTableId { get; set; }
        public string IpAddr { get; set; }
        public string CameraName { get; set; }
    }
}
