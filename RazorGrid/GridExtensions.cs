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
        public static MvcHtmlString NewTextBoxFor<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> expression)
        {
            IList<PropertyInfo> modelProperties = RazorGridHelper.ExtractGridModelProperties<TGridModel>();
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            IList<TGridModel> data = (IList<TGridModel>) metadata.Model;
            IList<Expression<Func<TModel, object>>> expressions = RazorGridHelper.GenerateExpressions<TModel, TGridModel>(modelProperties, data);
            //HtmlHelper<GridModel> ht = new HtmlHelper<GridModel>(htmlHelper.ViewContext, htmlHelper.ViewDataContainer);
            return RazorGridHelper.BuildGridBody<TModel>(htmlHelper, expressions);
        }
    }
}