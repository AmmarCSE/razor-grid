﻿using RazorGrid.Models;
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
    public static class GridTagHelper
    {
        public static string WrapInElement(string elementType, string innerHtml)
        {
            return WrapInElement(elementType: elementType, innerHtml: innerHtml, appendBreak: false);
        }

        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak)
        {
            return WrapInElement(elementType: elementType, 
                innerHtml: innerHtml,
                appendBreak: appendBreak,
                cssClass: string.Empty);
        }

        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak, string cssClass)
        {
            return WrapInElement(elementType: elementType, 
                innerHtml: innerHtml,
                appendBreak: appendBreak,
                cssClass: cssClass,
                htmlAttributes: null);
        }

        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak, string cssClass, object htmlAttributes)
        {
            return WrapInElementHelper(elementType: elementType, 
                innerHtml: innerHtml,
                appendBreak: appendBreak,
                cssClass: cssClass,
                htmlAttributes: htmlAttributes);
        }

        public static string WrapInElementHelper(string elementType, string innerHtml, bool appendBreak, string cssClass, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder(elementType);
            tagBuilder.InnerHtml = innerHtml;

            if(cssClass != string.Empty)
            {
                tagBuilder.AddCssClass(cssClass);
            }
            if(htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }

            string element = tagBuilder.ToString();
            if (appendBreak)
            {
                element += "<br/>";
            }

            return element;
        }
    }
}