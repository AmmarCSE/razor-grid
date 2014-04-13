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
        public static MvcHtmlString BuildGrid<TModel, TGridModel>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, List<TGridModel>>> gridExpression, 
            List<GridEnums.GridPermission> gridPermissions, 
            Dictionary<GridEnums.GridPermission, string> gridActions)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();

            IList<string> gridSections = new List<string>();

            gridSections.Add(ConstructActionBar(gridPermissions, gridActions));

            gridSections.Add(htmlHelper.ConstructHeaders(modelProperties, gridPermissions));
            gridSections.Add(htmlHelper.ConstructBody(modelProperties, ((IList<TGridModel>) metadata.Model).Count, gridPermissions));

            gridSections.Add(ConstructScripts(gridPermissions));

            StringBuilder builder = new StringBuilder();
            foreach (var section in gridSections)
            {
                builder.Append(section);
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        private static string ConstructActionBar(List<GridEnums.GridPermission> gridPermissions, Dictionary<GridEnums.GridPermission, string> gridActions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete))
            {
                builder.Append(GridCustomElement.DELETE_ICON);
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridCustomElement.ACTIVATE_ICON);
                builder.Append(GridCustomElement.UNACTIVATE_ICON);
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Add))
            {
                builder.Append(GridCustomElement.ADD_BUTTON(gridActions[GridEnums.GridPermission.Add]));
            }

            return builder.ToString();
        }

        private static string ConstructHeaders<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(htmlHelper.CheckBox("ToggleAll", new { onchange = GridCustomScript.CHECKBOX_TOGGLE("$(this)")}));
            }

            foreach (var property in properties)
            {
                TypeCode typeCode = Type.GetTypeCode(property.PropertyType);

                MvcHtmlString mvcLabel = null;
                switch (typeCode) { case TypeCode.String:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, string>(property));
                        break;
                    case TypeCode.Int32:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, int>(property));
                        break;
                }
                string header = WrapInElement("div", mvcLabel.ToString());
                builder.Append(header);
            }

            return WrapInElement("div", builder.ToString());
        }

        private static string ConstructBody<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, int numRows, List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < numRows; i++)
            {
                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
                {
                    builder.Append(htmlHelper.CheckBox("GridRowCheckBox"));
                }

                foreach (var property in properties)
                {
                    TypeCode typeCode = Type.GetTypeCode(property.PropertyType);

                    MvcHtmlString mvcTextBox = null;
                    
                    switch (typeCode)
                    {
                        case TypeCode.String:
                            mvcTextBox = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateBodyExpression<TModel, string>(property, i));
                            break;
                        case TypeCode.Int32:
                            mvcTextBox = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateBodyExpression<TModel, int>(property, i));
                            break;
                    }
                    builder.Append(mvcTextBox.ToString());
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Edit))
                {
                    builder.Append(GridCustomElement.DELETE_ICON);
                }
            }

            return WrapInElement("div", builder.ToString());
        }

        private static string ConstructScripts(List<GridEnums.GridPermission> gridPermissions)
        {
            StringBuilder builder = new StringBuilder();

            if (gridPermissions.Contains(GridEnums.GridPermission.Add) || 
                gridPermissions.Contains(GridEnums.GridPermission.Edit))
            {
                builder.Append(GridCustomScript.ACTION_REDIRECT_FUNCTION);
            }

            if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                gridPermissions.Contains(GridEnums.GridPermission.Update_Activation))
            {
                builder.Append(GridCustomScript.CHECKBOX_TOGGLE_FUNCTION);
            }
            return builder.ToString();
        }
        private static string WrapInElement(string elementType, string innerHtml)
        {
            TagBuilder tagBuilder = new TagBuilder(elementType);
            tagBuilder.InnerHtml = innerHtml;

            return tagBuilder.ToString();
        }
    }
}