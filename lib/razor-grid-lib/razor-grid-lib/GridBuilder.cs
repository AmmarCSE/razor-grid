using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Html
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

            string grid = GridTagHelper.WrapInElement("div", builder.ToString(), false, "grid");
            return MvcHtmlString.Create(grid);
        }

        private static string ConstructActionBar(List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
            {
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.DELETE_ICON, false, "btn"));
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.ACTIVATE_ICON, false, "btn"));
                builder.Append(GridTagHelper.WrapInElement("div", GridCustomElement.UNACTIVATE_ICON, false, "btn"));
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
                builder.Append(GridTagHelper.WrapInElement("div", htmlHelper.CheckBox("ToggleAll").ToString()));
            }

            foreach (var property in properties)
            {
                MvcHtmlString mvcLabel = htmlHelper.LabelElement(property);

                string header = GridTagHelper.WrapInElement("div", mvcLabel.ToString());
                builder.Append(header);
            }

            return GridTagHelper.WrapInElement("div", builder.ToString(), true, "gridHeader");
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
                    rowBuilder.Append(GridCustomElement.DELETE_ICON);
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