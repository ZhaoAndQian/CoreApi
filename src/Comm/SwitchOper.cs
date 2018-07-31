
namespace Preoff.Comm
{
    public class SwitchOper
    {
        public static string SwitchOperation(string _filter, FilterStr item)
        {
            switch (item.Operation)
            {
                case OperationStr.GreaterThan:
                    _filter += ">";
                    break;
                case OperationStr.LessThan:
                    _filter += "<";
                    break;
                case OperationStr.GreaterThanOrEqual:
                    _filter += ">=";
                    break;
                case OperationStr.LessThanOrEqual:
                    _filter += "<=";
                    break;
                case OperationStr.NotEqual:
                    _filter += "!=";
                    break;
                case OperationStr.Equal:
                    _filter += "==";
                    break;
                case OperationStr.Like:
                    _filter += ".Contains(\"";
                    break;
                default:
                    _filter += "==";
                    break;
            }

            return _filter;
        }
    }
}
