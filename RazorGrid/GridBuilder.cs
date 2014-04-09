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
    public static class GridBuilder
    {
        public static MvcHtmlString BuildGrid<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> gridExpression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();

            IList<Expression<Func<TModel, object>>> gridHeaderExpressions = 
                GridExpressionHelper.GenerateHeaderExpressions<TModel>(modelProperties);
            IList<Expression<Func<TModel, object>>> gridBodyExpressions = 
                GridExpressionHelper.GenerateBodyExpressions<TModel, TGridModel>(modelProperties, ((IList<TGridModel>) metadata.Model).Count);

            IList<string> gridSections = new List<string>();
            gridSections.Add(htmlHelper.ConstructHeaders(gridHeaderExpressions));
            gridSections.Add(htmlHelper.ConstructBody(gridBodyExpressions));

            StringBuilder builder = new StringBuilder();
            foreach (var section in gridSections)
            {
                builder.Append(section);
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        private static string ConstructHeaders<TModel>(this HtmlHelper<TModel> htmlHelper, IList<Expression<Func<TModel, object>>> expressions)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var expression in expressions)
            {
                builder.Append(htmlHelper.LabelFor(expression).ToString());
            }

            return builder.ToString();
        }

        private static string ConstructBody<TModel>(this HtmlHelper<TModel> htmlHelper, IList<Expression<Func<TModel, object>>> expressions)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var expression in expressions)
            {
                builder.Append(htmlHelper.TextBoxFor(expression).ToString());
            }

            return builder.ToString();
        }
    }
}