using System;
using System.Linq.Expressions;

namespace Catrobat.Core.Misc.Helpers
{
    public class PropertyHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }
    }
}
