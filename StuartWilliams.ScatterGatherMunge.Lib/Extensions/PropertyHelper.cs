using System;
using System.Linq.Expressions;
using System.Reflection;

namespace StuartWilliams.ScatterGatherMunge.Lib.Extensions
{

    /// <summary>
    /// Used to Get Property Info
    /// <para>
    /// From: <![CDATA[https://stackoverflow.com/questions/2051065/check-if-property-has-attribute]]>
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class PropertyHelper<T>
    {

        /// <summary>
        /// Get a property for a class
        /// <para>
        /// <c>
        /// <![CDATA[
        /// var property = PropertyHelper<MyClass>.GetProperty(x => x.MyProperty);
        /// ]]>
        /// </c>
        /// </para>
        /// <para>
        /// <c>
        /// <![CDATA[
        /// Attribute.IsDefined(property, typeof(MyPropertyAttribute));
        /// ]]>
        /// </c>
        /// </para>
        /// </summary>
        /// <typeparam name="TValue">Type</typeparam>
        /// <param name="selector">Func</param>
        /// <returns>Property</returns>
        /// <exception cref="InvalidOperationException">(sic)</exception>
        public static PropertyInfo GetProperty<TValue>(Expression<Func<T, TValue>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression expression)
            {
                body = expression.Body;
            }
            return body.NodeType switch
            {
                ExpressionType.MemberAccess => (PropertyInfo)((MemberExpression)body).Member,
                _ => throw new InvalidOperationException(),
            };
        }

    }
}
