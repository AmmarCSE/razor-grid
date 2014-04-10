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
        public static MvcHtmlString GridFor<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> expression)
        {
            return htmlHelper.GridFor(expression, gridPermissions: null);
        }

        public static MvcHtmlString GridFor<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> expression, List<GridEnums.GridPermission> gridPermissions)
        {
            return htmlHelper.GridHelper(expression, gridPermissions);
        }

        private static MvcHtmlString GridHelper<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> expression, List<GridEnums.GridPermission> gridPermissions)
        {
            return GridBuilder.BuildGrid(htmlHelper, expression, gridPermissions);
        }
    }
}