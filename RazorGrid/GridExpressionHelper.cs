using RazorGrid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;


namespace System.Web.Mvc.Html
{
    // Summary:
    //     Represents support for the HTML label element in an ASP.NET MVC view.
    public static class GridExpressionHelper
    {
        public static IList<Expression<Func<TModel, object>>> GenerateHeaderExpressions<TModel>(IList<PropertyInfo> properties)
        {
            IList<Expression<Func<TModel, object>>> expressions = new List<Expression<Func<TModel, object>>>();

            foreach (var property in properties)
            {
                ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");

                var dataListExpr = Expression.Property(fieldName, "Data");
                var itemExpr = Expression.Property(dataListExpr, "Item");
                var propertyExpr = Expression.Property(itemExpr, property.Name);

                Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(propertyExpr, fieldName);

                expressions.Add(exp);
            }

            return expressions;
        }

        public static List<Expression<Func<TModel, object>>> GenerateBodyExpressions<TModel, TGridModel>(IList<PropertyInfo> properties, int dataLength)
        {
            List<Expression<Func<TModel, object>>> expressions = new List<Expression<Func<TModel, object>>>();
            for (int i = 0; i < dataLength; i++)
            {
                foreach (var property in properties)
                {
                    ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");

                    var dataListExpr = Expression.Property(fieldName, "Data");
                    var itemExpr = Expression.Property(dataListExpr, "Item", Expression.Constant(i));
                    var propertyExpr = Expression.Property(itemExpr, property.Name);

                    Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(propertyExpr, fieldName);

                    expressions.Add(exp);
                }
            }
            return expressions;
        }
    }
}