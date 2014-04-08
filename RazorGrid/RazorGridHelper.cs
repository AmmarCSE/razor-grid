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
    public static class RazorGridHelper
    {
        public static IList<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static IList<Expression<Func<TModel, object>>> GenerateExpressions<TModel, TGridModel>(IList<PropertyInfo> properties, IList<TGridModel> data)
        {
            IList<Expression<Func<TModel, object>>> expressions = new List<Expression<Func<TModel, object>>>();
            for (int i = 0; i < data.Count; i++)
            {
                foreach (var property in properties)
                {
                    ParameterExpression fieldName = Expression.Parameter(typeof(TModel), "m");

                    var dataListExpr = Expression.Property(fieldName, "Data");
                    var itemExpr = Expression.Property(dataListExpr, "Item", Expression.Constant(i));
                    var propertyExpr = Expression.Property(itemExpr, property.Name);
                    //Expression fieldExpr1 = Expression.Convert(fieldExpr, typeof(object));
                    Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(propertyExpr, fieldName);

                    expressions.Add(exp);
                }
            }
            return expressions;
        }

        public static MvcHtmlString BuildGridBody<TGridModel>(this HtmlHelper<TGridModel> htmlHelper, IList<Expression<Func<TGridModel, object>>> expressions)
        {
            MvcHtmlString gridBody = new MvcHtmlString(String.Empty);
            StringBuilder builder = new StringBuilder();

            foreach (var expression in expressions)
            {
                builder.Append(htmlHelper.TextBoxFor(expression).ToString());
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}