using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Travel.Agency.RazorGrid.Helpers
{
    public static class GridElementHelper
    {
        public static MvcHtmlString LabelElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property)
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
        public static MvcHtmlString TextBoxElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property, int rowIndex)
        {
            bool isNullable;
            TypeCode typeCode = GridPropertyHelper.DetermineTypeCode(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, int?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, long?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, decimal?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case TypeCode.String:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, string>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, int>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, long>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.TextAreaFor(GridExpressionHelper.GenerateExpression<TModel, decimal>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }

            return mvcElement;
        }

        public static MvcHtmlString KeyElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property, int rowIndex)
        {
            bool isNullable;
            TypeCode typeCode = GridPropertyHelper.DetermineTypeCode(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, int?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, long?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, decimal?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case TypeCode.String:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, string>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int32:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, int>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Int64:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, long>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case TypeCode.Decimal:
                        mvcElement = htmlHelper.HiddenFor(GridExpressionHelper.GenerateExpression<TModel, decimal>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }

            return mvcElement;
        }
 
    }
}