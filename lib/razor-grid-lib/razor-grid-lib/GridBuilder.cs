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
            List<GridEnums.GridPermission> gridPermissions, 
            Dictionary<GridEnums.GridPermission, string> gridActions)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();

            IList<string> gridSections = new List<string>();

            gridSections.Add(ConstructActionBar(gridPermissions, gridActions));

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
                MvcHtmlString mvcLabel = htmlHelper.GenerateElement(property);

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
                    rowBuilder.Append(GridTagHelper.WrapInElement("div", htmlHelper.CheckBox("GridRowCheckBox").ToString()));
                }

                properties = GridPropertyHelper.ExtractNonKeyProperties(properties);
                foreach (var property in properties)
                {
                    MvcHtmlString mvcTextBox = htmlHelper.GenerateElement(property, i);
                    rowBuilder.Append(mvcTextBox.ToString());
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Edit))
                {
                    rowBuilder.Append(GridCustomElement.DELETE_ICON);
                }

                string row = GridTagHelper.WrapInElement("div", rowBuilder.ToString(), true, "gridRow");
                bodyBuilder.Append(row);
            }

            return GridTagHelper.WrapInElement("div", bodyBuilder.ToString(), false, "gridBody");
        }

        private static string ConstructPager()
        {
            return GridCustomElement.PAGER;
        }

        private static MvcHtmlString GenerateElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property)
        {
            bool isNullable;
            TypeCode typeCode = GridPropertyHelper.DetermineTypeCode(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, int?>(property, null));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, long?>(property, null));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, decimal?>(property, null));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case TypeCode.String:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, string>(property, null));
                        break;
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, int>(property, null));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, long>(property, null));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.LabelFor(GridExpressionHelper.GenerateExpression<TModel, decimal>(property, null));
                        break;
                }
            }

            return mvcElement;
        }

        private static MvcHtmlString GenerateElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property, int? rowIndex)
        {
            bool isNullable;
            TypeCode typeCode = GridPropertyHelper.DetermineTypeCode(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, int?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, long?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, decimal?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case TypeCode.String:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, string>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, int>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, long>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateExpression<TModel, decimal>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }

            return mvcElement;
        }
    }
}