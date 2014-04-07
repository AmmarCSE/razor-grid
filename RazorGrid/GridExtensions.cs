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

            IList<Expression<Func<TGridModel, object>>> expressions = RazorGridHelper.GenerateExpressions<TGridModel>(modelProperties);

            return RazorGridHelper.BuildGridBody<TModel>(htmlHelper, expressions);
        }
    }
}