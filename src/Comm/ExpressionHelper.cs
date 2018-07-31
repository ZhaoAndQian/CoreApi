using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Preoff.Comm
{
    public class ExpressionHelper
    {
        public static Type GetMemberType(MemberExpression expression)
        {
            var memberInfo = expression.Member;
            switch (memberInfo.MemberType)
            {
                case System.Reflection.MemberTypes.Field:
                    return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).PropertyType;
                default:
                    throw new Exception("未支持的成员类型：" + memberInfo.MemberType.ToString());
            }
        }
    }
}
