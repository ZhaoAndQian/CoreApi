
namespace Preoff.Comm
{
    public class FilterStr
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 搜索操作，大于小于等于
        /// </summary>
        public OperationStr Operation { get; set; }

        /// <summary>
        /// 搜索参数值
        /// </summary>
        public object Value { get; set; }
    }
}
