
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preoff.Entity
{
    public partial class PermissonTable
    {
        public int Id { get; set; }
        public int? PId { get; set; }
        public string PermissonName { get; set; }
        public string PermissonDesc { get; set; }
        public string RouterName { get; set; }
        public int? PermissonSeq { get; set; }
        public string Icon { get; set; }
        /// <summary>
        /// 供二级菜单使用
        /// </summary>
        [NotMapped]
        public List<PermissonTable> Childrens { get; set; }
    }
}
