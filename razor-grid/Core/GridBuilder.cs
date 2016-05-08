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

namespace AmmarCSE.RazorGrid.Core
{
    public static class GridBuilder
    {
        public static MvcHtmlString BuildGrid<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, List<TGridModel>>> gridExpression, 
            List<GridEnums.GridPermission> gridPermissions)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();

            IList<string> gridSections = new List<string>();

            gridSections.Add(ConstructActionBar(gridPermissions));

            gridSections.Add(htmlHelper.ConstructHeaders(GridPropertyHelper.ExtractNonKeyProperties(modelProperties), gridPermissions));
            gridSections.Add(htmlHelper.ConstructBody(modelProperties, ((IList<TGridModel>) metadata.Model).Count, gridPermissions));

            gridSections.Add(ConstructPager());

            StringBuilder builder = new StringBuilder();
            foreach (var section in gridSections)
            {
                builder.Append(section);
            }
            
            string grid = GridTagHelper.WrapInElement("div", builder.ToString(), false, "grid", GridPropertyHelper.RetrieveActionAttributes<TGridModel>());
            return MvcHtmlString.Create(grid);
        }

        private static string ConstructActionBar(List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
            {
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.DELETE_ICON, false, "btn gridDelete"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.ACTIVATE_ICON, false, "btn gridActivate"));
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.UNACTIVATE_ICON, false, "btn gridUnactivate"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Add))
            {
                builder.Append(GridTagHelper.WrapInElement("span", GridCustomElement.ADD_BUTTON, false, "gridAdd"));
            }

            return GridTagHelper.WrapInElement("div", builder.ToString(), true);
        }

        private static string ConstructHeaders<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridTagHelper.WrapInElement("div", htmlHelper.CheckBox("triggerToggleAll").ToString(), false, "ToggleAll"));
            }

            foreach (var property in properties)
            {
                MvcHtmlString mvcLabel = htmlHelper.LabelElement(property);

                string header = GridTagHelper.WrapInElement("div", mvcLabel.ToString(), false, "", GridPropertyHelper.RetrieveHtmlAttributes(property));
                builder.Append(header);
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                gridPermissions.Contains(GridEnums.GridPermission.Edit))
            {
                builder.Append(GridTagHelper.WrapInElement("div", "", false, "actionColumn"));
            }
            return GridTagHelper.WrapInElement("div", builder.ToString(), false, "gridHeader");
        }

        private static string ConstructBody<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, int numRows, List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder bodyBuilder = new StringBuilder();

            for (int i = 0; i < numRows; i++)
            {
                StringBuilder rowBuilder = new StringBuilder();

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
                {
                    rowBuilder.Append(GridTagHelper.WrapInElement("div", htmlHelper.CheckBox("CheckBox").ToString(), false, "gridRowCheckBox"));
                }

                var keys = GridPropertyHelper.ExtractKeyProperties(properties);
                foreach (var key in keys)
                {
                    MvcHtmlString mvcTextBox = htmlHelper.KeyElement(key, i);
                    rowBuilder.Append(mvcTextBox.ToString());
                }

                var fields = GridPropertyHelper.ExtractNonKeyProperties(properties);
                foreach (var field in fields)
                {
                    MvcHtmlString mvcTextBox = htmlHelper.TextBoxElement(field, i);
                    rowBuilder.Append(mvcTextBox.ToString());
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Edit))
                {
                    string actions = "";
                    if (gridPermissions.Contains(GridEnums.GridPermission.Edit))
                    {
                        actions += GridCustomElement.EDIT_ICON;
                    }
                    if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
                    {
                        actions += GridCustomElement.DELETE_ICON;
                    }
                    rowBuilder.Append(GridTagHelper.WrapInElement("div", actions, false, "actionColumn"));
                }

                string row = GridTagHelper.WrapInElement("div", rowBuilder.ToString(), false, "gridRow");
                bodyBuilder.Append(row);
            }

            return GridTagHelper.WrapInElement("div", bodyBuilder.ToString(), false, "gridBody");
        }

        private static string ConstructPager()
        {
            return GridCustomElement.PAGER;
        }
    }
}