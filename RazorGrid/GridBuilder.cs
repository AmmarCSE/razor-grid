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
        public static MvcHtmlString BuildGrid<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TGridModel>>> gridExpression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(gridExpression, htmlHelper.ViewData);
            
            IList<PropertyInfo> modelProperties = GridPropertyHelper.ExtractGridModelProperties<TGridModel>();

            IList<string> gridSections = new List<string>();
            gridSections.Add(htmlHelper.ConstructHeaders(modelProperties));
            gridSections.Add(htmlHelper.ConstructBody(modelProperties, ((IList<TGridModel>) metadata.Model).Count));

            StringBuilder builder = new StringBuilder();
            foreach (var section in gridSections)
            {
                builder.Append(section);
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        private static string ConstructHeaders<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var property in properties)
            {
                TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
                MvcHtmlString mvcLabel = null;
                switch (typeCode)
                {
                    case TypeCode.String:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, string>(property));
                        break;
                    case TypeCode.Int32:
                        mvcLabel = htmlHelper.LabelFor(GridExpressionHelper.GenerateHeaderExpression<TModel, int>(property));
                        break;
                }

                builder.Append(mvcLabel.ToString());
            }

            return builder.ToString();
        }

        private static string ConstructBody<TModel>(this HtmlHelper<TModel> htmlHelper, IList<PropertyInfo> properties, int numRows)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < numRows; i++)
            {
                foreach (var property in properties)
                {
                    TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
                    MvcHtmlString mvcLabel = null;
                    switch (typeCode)
                    {
                        case TypeCode.String:
                            mvcLabel = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateBodyExpression<TModel, string>(property, i));
                            break;
                        case TypeCode.Int32:
                            mvcLabel = htmlHelper.TextBoxFor(GridExpressionHelper.GenerateBodyExpression<TModel, int>(property, i));
                            break;
                    }

                    builder.Append(mvcLabel.ToString());
                }
            }

            return builder.ToString();
        }
    }
}