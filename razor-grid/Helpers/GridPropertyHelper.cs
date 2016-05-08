﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using Travel.Agency.RazorGrid.GridAttributes;

namespace Travel.Agency.RazorGrid.Helpers
{
    public static class GridPropertyHelper
    {
        public static IList<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static IList<PropertyInfo> ExtractKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static IList<PropertyInfo> ExtractNonKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static Dictionary<string, object> RetrieveHtmlAttributes(PropertyInfo property)
        {
            Dictionary<string, object> htmlAttributes = RetrieveParentHtmlAttributes(property);
            GridHtmlAttribute[] attributes = (GridHtmlAttribute[])property.GetCustomAttributes(typeof(GridHtmlAttribute), false);

            foreach (var attribute in attributes)
            {
                htmlAttributes.Add(attribute.Attr, attribute.AttrVal);
            }
            return htmlAttributes;
        }

        public static Dictionary<string, object> RetrieveParentHtmlAttributes(PropertyInfo property)
        {
            Type t = property.ReflectedType;
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            GridReadonlyAttribute[] attributes = (GridReadonlyAttribute[])t.GetCustomAttributes(typeof(GridReadonlyAttribute), false);

            if (attributes.Length > 0)
            {
                htmlAttributes.Add("readonly", "readonly");
            }

            return htmlAttributes;
        }

        public static Dictionary<string, object> RetrieveActionAttributes<TGridModel>()
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            GridActionAttribute[] attributes = (GridActionAttribute[])typeof(TGridModel).GetCustomAttributes(typeof(GridActionAttribute), false);

            foreach (var attribute in attributes)
            {
                htmlAttributes.Add(attribute.Action, attribute.ActionVal);
            }
            return htmlAttributes;
        }
        
        public static TypeCode DetermineTypeCode(PropertyInfo property, out bool isNullable)
        {
            isNullable = false;
            TypeCode typeCode;
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                typeCode = Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]);
                isNullable = true;
            }
            else
            {
                typeCode = Type.GetTypeCode(property.PropertyType);
            }

            return typeCode;
        }
    }
}