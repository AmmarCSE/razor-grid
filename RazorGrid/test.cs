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
    // Summary:
    //     Represents support for the HTML label element in an ASP.NET MVC view.
    public static class GridExtensions
    {
        public static MvcHtmlString NewTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object instance)
        {
            
            object lm = instance;
            lm = lm.GetType();//.GetProperties();
            IList<PropertyInfo> props = ExtractGridModelProperties<TProperty>();

            IList<Expression<Func<TModel, object>>> exps = GenerateExpressions<TModel>(props, lm);

            return htmlHelper.TextBoxFor(exps[0]);
        }

        public static IList<PropertyInfo> ExtractGridModelProperties<TProperty>()
        {
            Type myType = typeof(TProperty).GetProperty("Item").PropertyType;
            return new List<PropertyInfo>(myType.GetProperties());
        }

        public static IList<Expression<Func<TModel, object>>> GenerateExpressions<TModel>(IList<PropertyInfo> properties, object lm)
        {
            IList<Expression<Func<TModel, object>>> expressions = new List<Expression<Func<TModel, object>>>();
            foreach (var property in properties)
            {
                ParameterExpression fieldName = Expression.Parameter(typeof(object), property.Name);
                Expression fieldExpr = Expression.PropertyOrField(Expression.Constant(lm), property.Name);
                Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(fieldExpr, fieldName);

                expressions.Add(exp);
            } 
            
            return expressions;
        }
    }
}