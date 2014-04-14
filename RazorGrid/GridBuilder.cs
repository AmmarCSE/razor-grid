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

<<<<<<< HEAD
=======
            gridSections.Add(ConstructPager());

>>>>>>> 150eee121101facc93ef772c5493575f783a0130
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
                TypeCode typeCode = Type.GetTypeCode(property.PropertyType);

                MvcHtmlString mvcLabel = null;
                switch (typeCode) { 
                    case TypeCode.String:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, string>(property));
                        break;
                    case TypeCode.Int32:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, int>(property));
                        break;
                }
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
                    rowBuilder.Append(mvcTextBox.ToString());
                }

                if (gridPermissions.Contains(GridEnums.GridPermission.Delete) || 
                    gridPermissions.Contains(GridEnums.GridPermission.Edit))
                {
                    rowBuilder.Append(GridCustomElement.DELETE_ICON);
                }
<<<<<<< HEAD

                string row = GridTagHelper.WrapInElement("div", rowBuilder.ToString(), true, "gridRow");
                bodyBuilder.Append(row);
            }

            return GridTagHelper.WrapInElement("div", bodyBuilder.ToString(), false, "gridBody");
=======
            }

            return WrapInElement("div", builder.ToString());
        }

        private static string ConstructPager()
        {
            return GridCustomElement.PAGER;
>>>>>>> 150eee121101facc93ef772c5493575f783a0130
        }

    }
}