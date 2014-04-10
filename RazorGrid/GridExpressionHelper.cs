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
        public static Expression<Func<TModel, TProperty>> GenerateHeaderExpression<TModel, TProperty>(PropertyInfo property)
        {
            ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");

            var dataListExpr = Expression.Property(fieldName, "Data");
            var itemExpr = Expression.Property(dataListExpr, "Item");
            var propertyExpr = Expression.Property(itemExpr, property.Name);

            return Expression.Lambda<Func<TModel, TProperty>>(propertyExpr, fieldName);
        }

        public static Expression<Func<TModel, TProperty>> GenerateBodyExpression<TModel, TProperty>(PropertyInfo property, int rowIndex)
        {
            ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");

            var dataListExpr = Expression.Property(fieldName, "Data");
            var itemExpr = Expression.Property(dataListExpr, "Item", Expression.Constant(rowIndex));
            var propertyExpr = Expression.Property(itemExpr, property.Name);

            return Expression.Lambda<Func<TModel, TProperty>>(propertyExpr, fieldName);
        }
    }
}