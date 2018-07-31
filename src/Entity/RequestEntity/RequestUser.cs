using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preoff.Entity.RequestEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestUser : BaseRequest
    {
        public string LoginName { get; set; }

        public string RealName { get; set; }
    }
}
