
namespace Preoff.Comm
{
    public class SqlFilter
    {
        public static SqlFilter Create(string propertyName, Operation operation, object value)
        {
            return new SqlFilter()
            {
                Name = propertyName,
                Operation = operation,
                Value = value
            };
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 搜索操作，大于小于等于
        /// </summary>
        public Operation Operation { get; set; }

        /// <summary>
        /// 搜索参数值
        /// </summary>
        public object Value { get; set; }
    }


    public class Sort
    {
        public string Field { get; set; }
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 将排序规则转换为sql语句
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Field, SortOrder.ToString());
        }
    }
    public enum SortOrder
    {
        DESCENDING, ASC
    }
}
