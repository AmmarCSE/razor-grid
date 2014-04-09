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
        public static MvcHtmlString BuildGrid<TModel>(this HtmlHelper<TModel> htmlHelper, IList<Expression<Func<TModel, object>>> expressions)
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