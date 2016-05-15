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
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;
using AmmarCSE.RazorGrid.Resources;
using AmmarCSE.RazorGrid.Helpers;
using System.Runtime.Serialization;

namespace AmmarCSE.RazorGrid.Core
{
    public static class GridBuilder
    {
        public static MvcHtmlString BuildGrid<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, List<TGridModel>>> gridExpression,
            List<GridEnums.GridPermission> gridPermissions,
            bool isSearch)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();
            IList<string> gridSections = new List<string>();

            gridSections.Add(htmlHelper.ConstructValidationMessages(GridPropertyHelper.ExtractNonKeyProperties(modelProperties)));
            gridSections.Add(htmlHelper.ConstructHeaders(GridPropertyHelper.ExtractNonKeyProperties(modelProperties), gridPermissions));
            gridSections.Add(htmlHelper.ConstructBody(modelProperties, (IList<TGridModel>)metadata.Model, gridPermissions));

            int pageCount = 0;
            if (htmlHelper.ViewData.ModelMetadata.Properties.Any(w => w.PropertyName == "PageCount"))
            {
                dynamic dummyCast = htmlHelper.ViewData.Model;
                pageCount = dummyCast.PageCount;
            }

            gridSections.Add(ConstructActionBar(gridPermissions, pageCount));

            StringBuilder builder = new StringBuilder();
            foreach (var section in gridSections)
            {
                builder.Append(section);
            }

            string grid = GridTagHelper.WrapInElement(
                "form",
                builder.ToString(),
                false,
                "grid",
                GridPropertyHelper.RetrieveActionAttributes<TGridModel>());

            return MvcHtmlString.Create(grid);
        }

        private static string ConstructActionBar(List<GridEnums.GridPermission> gridPermissions, int pageCount)
        {
            StringBuilder builder = new StringBuilder();
            bool isEmpty = false;

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
            {
                isEmpty = true;
                builder.Append(GridTagHelper.WrapInElement("span", GridCustomElement.DELETE_ICON, false, "gridDelete actionButton"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                isEmpty = true;
                builder.Append(GridTagHelper.WrapInElement("span", GridCustomElement.ACTIVATE_ICON, false, "gridActivate actionButton"));
                builder.Append(GridTagHelper.WrapInElement("span", GridCustomElement.UNACTIVATE_ICON, false, "gridUnactivate actionButton"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Add))
            {
                isEmpty = true;
                builder.Append(GridTagHelper.WrapInElement("span", GridCustomElement.ADD_BUTTON, false, "gridAdd"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Lock))
            {
                isEmpty = true;
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.LOCK_ICON, false, "btn gridLock"));
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.UNLOCK_ICON, false, "btn gridUnlock"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.ResetPassword))
            {
                isEmpty = true;
                Dictionary<string, object> resetAttr = new Dictionary<string, object>();
                resetAttr.Add("data-parhold-id", "IdsResetPassword");
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.RESETPASSWORD_ICON, false, "btn gridResetPassword", resetAttr));
            }

            if (pageCount > 0)
            {
                isEmpty = true;
                builder.Append(GridTagHelper.WrapInElement("span", ConstructPager(pageCount), false, "pager"));
            }

            if (isEmpty == false)
            {
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.Empty_Div, false, "emptyDiv"));
            }

            return GridTagHelper.WrapInElement("div", builder.ToString(), false);
        }

        private static string ConstructValidationMessages<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var property in properties)
            {
                MvcHtmlString mvcValidationMessage = htmlHelper.ValidationElement(property);
                builder.Append(GridTagHelper.WrapInElement("div", mvcValidationMessage.ToString(), false));
            }

            return builder.ToString();
        }

        private static string ConstructHeaders<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();
            string bottomBoderDiv = GridTagHelper.WrapInElement("div", string.Empty, false, "divBottomBorder");

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) ||
                gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridTagHelper.WrapInElement("div", htmlHelper.CheckBox("triggerToggleAll") + bottomBoderDiv, false, "ToggleAll"));
            }

            foreach (var property in properties)
            {
                MvcHtmlString mvcLabel = htmlHelper.LabelElement(property);

                string header = GridTagHelper.WrapInElement("div", mvcLabel + bottomBoderDiv, false, string.Empty, GridPropertyHelper.RetrieveHtmlAttributes(property));
                builder.Append(header);
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) ||
                gridPermissions.Contains(GridEnums.GridPermission.Edit))
            {
                builder.Append(GridTagHelper.WrapInElement("div", "<label>Action</label>" + bottomBoderDiv, false, "actionColumn"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Details))
            {
                builder.Append(GridTagHelper.WrapInElement("div", "<label>Details</label>" + bottomBoderDiv, false, "actionColumn"));
            }

            return GridTagHelper.WrapInElement("div", builder.ToString(), false, "gridHeader");
        }

        private static string ConstructBody<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper,
            IList<PropertyInfo> properties,
            IList<TGridModel> Data,
            List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder bodyBuilder = new StringBuilder();

            TGridModel instance = (TGridModel)FormatterServices.GetUninitializedObject(typeof(TGridModel));
            Data.Insert(0, instance);

            int numRows = Data.Count();
            for (int i = 0; i < numRows; i++)
            {
                StringBuilder rowBuilder = new StringBuilder();
                IList<PropertyInfo> fields = GridPropertyHelper.ExtractNonKeyProperties(properties);

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete)
                    || gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
                {
                    rowBuilder.Append(
                        GridTagHelper.WrapInElement(
                            "div", htmlHelper.CheckBox("CheckBox").ToString(), false, "gridRowCheckBox"));
                }

                var keys = GridPropertyHelper.ExtractKeyProperties(properties);
                foreach (var key in keys)
                {
                    MvcHtmlString mvcTextBox = htmlHelper.KeyElement(key, i);
                    rowBuilder.Append(mvcTextBox);
                }

                foreach (var field in fields)
                {
                    string strClasses = "gridRowVisibleCell" + GridPropertyHelper.GetExtraCssClass(Data, i, field.Name);

                    rowBuilder.Append(
                        GridTagHelper.WrapInElement(
                            "div", htmlHelper.TextBoxElement(field, i, Data).ToString(), false, strClasses, GridPropertyHelper.RetrieveHtmlAttributes(field)));
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete)
                    || gridPermissions.Contains(GridEnums.GridPermission.Edit))
                {
                    string actions = string.Empty;
                    if (gridPermissions.Contains(GridEnums.GridPermission.Edit))
                    {
                        actions += GridCustomElement.EDIT_ICON;
                    }

                    if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
                    {
                        // actions += GridTagHelper.WrapInElement("div", GridCustomElement.DELETE_ICON, false, "deleteRow");
                    }

                    rowBuilder.Append(GridTagHelper.WrapInElement("div", actions, false, "actionColumn"));
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Details))
                {
                    string actions = string.Empty;
                    actions += GridTagHelper.WrapInElement("div", GridCustomElement.DETAILS_ICON, false, "rowDetails");
                    rowBuilder.Append(GridTagHelper.WrapInElement("div", actions, false, "actionColumn"));
                }

                string row = GridTagHelper.WrapInElement("div", rowBuilder.ToString(), false, "gridRow");
                bodyBuilder.Append(row);

                if (Data.Count == 1)
                {
                    string message = GridTagHelper.WrapInElement("div", "No results were found", false, "no-results");
                    bodyBuilder.Append(message);
                }
            }

            return GridTagHelper.WrapInElement("div", bodyBuilder.ToString(), false, "gridBody");
        }

        private static string ConstructPager(int pageCount)
        {
            StringBuilder pagerBuilder = new StringBuilder();
            pagerBuilder.Append(GridCustomElement.PAGER_BUTTON_PREV);

            pagerBuilder.Append("<button type=\"button\" data-page-index=\"0\" class=\"page selected\">");
            pagerBuilder.Append("1");
            pagerBuilder.Append("</button>");

            int initialCount = pageCount > 4 ? 5 : pageCount;

            for (int i = 1; i < initialCount; i++)
            {
                pagerBuilder.Append("<button type=\"button\" data-page-index=\"" + i + "\" class=\"page\">");
                pagerBuilder.Append((i + 1).ToString());
                pagerBuilder.Append("</button>");
            }

            int remainingCounter = 5;
            if (pageCount > 10)
            {
                remainingCounter = pageCount - 5;
                pagerBuilder.Append("<span class=\"dots\">...</span>");
            }

            for (; remainingCounter < pageCount; remainingCounter++)
            {
                pagerBuilder.Append("<button type=\"button\" data-page-index=\"" + remainingCounter + "\" class=\"page\">");
                pagerBuilder.Append((remainingCounter + 1).ToString());
                pagerBuilder.Append("</button>");
            }

            string disabled = pageCount < 2 ? "disabled=\"disabled\"" : string.Empty;
            pagerBuilder.Append(string.Format(GridCustomElement.PAGER_BUTTON_NEXT, disabled));

            return pagerBuilder.ToString();
        }
    }
}