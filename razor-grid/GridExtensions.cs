using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

using AmmarCSE.RazorGrid.Core;
using AmmarCSE.RazorGrid.Resources;

namespace System.Web.Mvc.Html
{
    public static class GridExtensions
    {
        public static MvcHtmlString GridFor<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, List<TGridModel>>> expression,
            List<GridEnums.GridPermission> gridPermissions = null,
            bool isSearch = false)
        {
            gridPermissions = gridPermissions ?? new List<GridEnums.GridPermission>();
            return htmlHelper.GridHelper(expression, gridPermissions, isSearch);
        }

        private static MvcHtmlString GridHelper<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, List<TGridModel>>> expression,
            List<GridEnums.GridPermission> gridPermissions,
            bool isSearch)
        {
            return GridBuilder.BuildGrid(htmlHelper, expression, gridPermissions, isSearch);
        }
    }
}