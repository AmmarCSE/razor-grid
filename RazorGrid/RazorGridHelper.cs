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
            //Type myType = .GetProperty("Item").PropertyType;
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static IList<Expression<Func<TGridModel, object>>> GenerateExpressions<TGridModel>(IList<PropertyInfo> properties)
        {
            IList<Expression<Func<TGridModel, object>>> expressions = new List<Expression<Func<TGridModel, object>>>();
            foreach (var property in properties)
            {
                //Expression fieldExpr = Expression.PropertyOrField(Expression.Parameter(typeof(string).GetProperty("Item").PropertyType, "o"), property.Name);
                //ParameterExpression fieldName = Expression.Parameter(typeof(string).GetProperty("Item").PropertyType, property.Name);
                ParameterExpression fieldName = Expression.Parameter(typeof(TGridModel), "i");
                MemberExpression fieldExpr = Expression.Property(fieldName, property.Name);
                Expression<Func<TGridModel, object>> expression = Expression.Lambda<Func<TGridModel, object>>(fieldExpr, fieldName);

                expressions.Add(expression);
            } 
            
            return expressions;
        }
        
        public static MvcHtmlString BuildGridBody<TModel>(this HtmlHelper<TModel> htmlHelper, IList<Expression<Func<TModel, object>>> expressions)
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