using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Entity.RequestEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestExecTask:BaseRequest
    {
        public int? UserTableId { get; set; }
        public int? TaskStateTableId { get; set; }
    }
}
