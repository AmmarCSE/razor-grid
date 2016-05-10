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
        /// <summary>
        /// The grid for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="gridPermissions">
        /// The grid permissions.
        /// </param>
        /// <param name="isSearch">
        /// The is search.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString GridFor<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, List<TGridModel>>> expression,
            List<GridEnums.GridPermission> gridPermissions = null,
            bool isSearch = false)
        {
            gridPermissions = gridPermissions ?? new List<GridEnums.GridPermission>();
            return htmlHelper.GridHelper(expression, gridPermissions, isSearch);
        }

        /// <summary>
        /// The grid helper.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="gridPermissions">
        /// The grid permissions.
        /// </param>
        /// <param name="isSearch">
        /// The is search.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
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