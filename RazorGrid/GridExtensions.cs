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
    public static class GridExtensions
    {
        public static MvcHtmlString NewTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, GridModel instance)
        {
            GridModel lm = instance;
            PropertyInfo t1 = typeof(TModel).GetProperty("UserName");
            PropertyInfo t2 = lm.GetType().GetProperty("UserName");
            IList<PropertyInfo> props = ExtractGridModelProperties<TProperty>();

            IList<Expression<Func<TModel, object>>> expressions = GenerateExpressions<TModel>(props, lm);

            return BuildGridBody<TModel>(htmlHelper, expressions);
        }

        public static IList<PropertyInfo> ExtractGridModelProperties<TProperty>()
        {
            Type myType = typeof(TProperty).GetProperty("Item").PropertyType;
            return new List<PropertyInfo>(myType.GetProperties());
        }

        public static IList<Expression<Func<TModel, object>>> GenerateExpressions<TModel>(IList<PropertyInfo> properties, object lm)
        {
            IList<Expression<Func<TModel, object>>> expressions = new List<Expression<Func<TModel, object>>>();
            foreach (var property in properties)
            {
                ParameterExpression fieldName = Expression.Parameter(typeof(object), property.Name);
                Expression fieldExpr = Expression.PropertyOrField(Expression.Constant(lm), property.Name);
                Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(fieldExpr, fieldName);

                expressions.Add(exp);
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