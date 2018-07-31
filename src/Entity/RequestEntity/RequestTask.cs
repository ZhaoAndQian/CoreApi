using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Entity.RequestEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestTask:BaseRequest
    {
        public string TaskName { get; set; }
        public int? TaskTypeTableId { get; set; }
    }
}
