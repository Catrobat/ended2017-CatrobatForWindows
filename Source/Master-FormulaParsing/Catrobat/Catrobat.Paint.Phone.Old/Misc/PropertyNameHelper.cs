using System;
using System.Linq.Expressions;

namespace Catrobat.Paint.Phone.Old.Misc
{
    public class PropertyNameHelper
    {
        public static string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
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
