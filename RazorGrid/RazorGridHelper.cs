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

        public static IList<Expression<Func<TGridModel, object>>> GenerateExpressions<TGridModel>(IList<PropertyInfo> properties, object lm)
        {
            IList<Expression<Func<TGridModel, object>>> expressions = new List<Expression<Func<TGridModel, object>>>();
            foreach (var property in properties)
            {
                ParameterExpression fieldName = Expression.Parameter(typeof(TGridModel), "i");
                MemberExpression fieldExpr = Expression.Property(fieldName, property.Name);
                //Expression fieldExpr1 = Expression.Convert(fieldExpr, typeof(object));
                Expression<Func<TGridModel, object>> exp = Expression.Lambda<Func<TGridModel, object>>(fieldExpr, fieldName);

                expressions.Add(exp);
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