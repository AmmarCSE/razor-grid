using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;


namespace System.Web.Mvc.Html
{
    // Summary:
    //     Represents support for the HTML label element in an ASP.NET MVC view.
    public static class GridExtensions
    {
        public static MvcHtmlString NewTextBoxFor<TModel, TProperty>(this NewHTMLHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.NewTextBoxFor(expression, format: null);
        }
        public static MvcHtmlString NewTextBoxFor<TModel, TProperty>(this NewHTMLHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            return htmlHelper.NewTextBoxFor(expression, format, (IDictionary<string, object>)null);
        }
        public static MvcHtmlString NewTextBoxFor<TModel, TProperty>(this NewHTMLHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return TextBoxHelper(htmlHelper,
                                 metadata,
                                 metadata.Model,
                                 ExpressionHelper.GetExpressionText(expression),
                                 format,
                                 htmlAttributes);
        }
        private static MvcHtmlString TextBoxHelper(this NewHTMLHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(htmlHelper,
                               InputType.Text,
                               metadata,
                               expression,
                               model,
                               useViewData: false,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: format,
                               htmlAttributes: htmlAttributes);
        }
        private static MvcHtmlString InputHelper(NewHTMLHelper htmlHelper, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                //throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", NewHTMLHelper.GetInputTypeString(inputType));
            tagBuilder.MergeAttribute("name", fullName, true);

            string valueParameter = htmlHelper.FormatValue(value, format);
            bool usedModelState = false;

            switch (inputType)
            {
                case InputType.CheckBox:
                    bool? modelStateWasChecked = htmlHelper.GetModelStateValue(fullName, typeof(bool)) as bool?;
                    if (modelStateWasChecked.HasValue)
                    {
                        isChecked = modelStateWasChecked.Value;
                        usedModelState = true;
                    }
                    goto case InputType.Radio;
                case InputType.Radio:
                    if (!usedModelState)
                    {
                        string modelStateValue = htmlHelper.GetModelStateValue(fullName, typeof(string)) as string;
                        if (modelStateValue != null)
                        {
                            isChecked = String.Equals(modelStateValue, valueParameter, StringComparison.Ordinal);
                            usedModelState = true;
                        }
                    }
                    if (!usedModelState && useViewData)
                    {
                        isChecked = htmlHelper.EvalBoolean(fullName);
                    }
                    if (isChecked)
                    {
                        tagBuilder.MergeAttribute("checked", "checked");
                    }
                    tagBuilder.MergeAttribute("value", valueParameter, isExplicitValue);
                    break;
                case InputType.Password:
                    if (value != null)
                    {
                        tagBuilder.MergeAttribute("value", valueParameter, isExplicitValue);
                    }
                    break;
                default:
                    string attemptedValue = (string)htmlHelper.GetModelStateValue(fullName, typeof(string));
                    tagBuilder.MergeAttribute("value", attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(fullName, format) : valueParameter), isExplicitValue);
                    break;
            }

            if (setId)
            {
                tagBuilder.GenerateId(fullName);
            }

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(NewHTMLHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            if (inputType == InputType.CheckBox)
            {
                // Render an additional <input type="hidden".../> for checkboxes. This
                // addresses scenarios where unchecked checkboxes are not sent in the request.
                // Sending a hidden input makes it possible to know that the checkbox was present
                // on the page when the request was submitted.
                StringBuilder inputItemBuilder = new StringBuilder();
                inputItemBuilder.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));

                TagBuilder hiddenInput = new TagBuilder("input");
                hiddenInput.MergeAttribute("type", NewHTMLHelper.GetInputTypeString(InputType.Hidden));
                hiddenInput.MergeAttribute("name", fullName);
                hiddenInput.MergeAttribute("value", "false");
                inputItemBuilder.Append(hiddenInput.ToString(TagRenderMode.SelfClosing));
                return MvcHtmlString.Create(inputItemBuilder.ToString());
            }

            return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
        }
        //static object GetModelStateValue(string key, Type destinationType)
        //{
        //    ModelState modelState;
        //    if (ViewData.ModelState.TryGetValue(key, out modelState))
        //    {
        //        if (modelState.Value != null)
        //        {
        //            return modelState.Value.ConvertTo(destinationType, null /* culture */);
        //        }
        //    }
        //    return null;
        //}
        //static bool EvalBoolean(string key)
        //{
        //    return Convert.ToBoolean(ViewData.Eval(key), CultureInfo.InvariantCulture);
        //}
        //static string EvalString(string key, string format)
        //{
        //    return Convert.ToString(ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        //}
        //static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        //{
        //    Debug.Assert(tagBuilder != null);
        //    return new MvcHtmlString(tagBuilder.ToString(renderMode));
        //}
        //public static ViewDataDictionary ViewData
        //{
        //    get { return null;}// ViewDataContainer.ViewData; }
        //}
        //public static IViewDataContainer ViewDataContainer { get; internal set; }
    }
}