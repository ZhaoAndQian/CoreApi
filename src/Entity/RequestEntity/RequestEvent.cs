using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Entity.RequestEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestEvent : BaseRequest
    {
        public int? ExecTaskTableId { get; set; }
        public int? EventTypeTableId { get; set; }
    }
}
