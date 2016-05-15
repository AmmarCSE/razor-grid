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
using AmmarCSE.RazorGrid.Attributes;

namespace AmmarCSE.RazorGrid.Helpers
{
    public static class GridPropertyHelper
    {
        public static List<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static List<PropertyInfo> ExtractGridModelSelectProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        public static IList<PropertyInfo> ExtractKeyProperties(IList<PropertyInfo> properties)
        {
            return properties.Where(p => Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static IList<PropertyInfo> ExtractNonKeyProperties(IList<PropertyInfo> properties)
        {
            return properties.Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        public static List<PropertyInfo> ExtractQuickSearchProperties<TModel>()
        {
            return null;// typeof(TModel).GetProperties().Where(p => Attribute.IsDefined(p, typeof(QuickSearchAttribute), false)).ToList();
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
            // Type t = property.ReflectedType;
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            /*GridReadonlyAttribute[] attributes = (GridReadonlyAttribute[])t.GetCustomAttributes(typeof(GridReadonlyAttribute), false);
            if (attributes.Length > 0)
            {*/
                htmlAttributes.Add("readonly", "readonly");

            // }
            return htmlAttributes;
        }

        public static Dictionary<string, object> RetrieveActionAttributes<TGridModel>()
        {
            GridActionAttribute[] attributes =
                (GridActionAttribute[])typeof(TGridModel).GetCustomAttributes(typeof(GridActionAttribute), false);

            return attributes.ToDictionary(attribute => attribute.Action, attribute => attribute.ActionVal);
        }

        public static dynamic RetrieveGridEntityPropertyPath(PropertyInfo property)
        {
            return null;
        }

        public static int DetermineTypeCode(PropertyInfo property)
        {
            bool isNullable; // dummy variable
            return DetermineTypeCodeAndNullability(property, out isNullable);
        }

        public static int DetermineTypeCodeAndNullability(PropertyInfo property, out bool isNullable)
        {
            isNullable = false;
            TypeCode typeCode;
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                typeCode = Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]);
                isNullable = true;
            }
            else
            {
                typeCode = Type.GetTypeCode(property.PropertyType);
            }

            if (typeCode == TypeCode.Object && property.PropertyType.Name == "TimeSpan")
            {
                return 17;
            }

            return (int)typeCode;
        }

        public static string GetExtraCssClass<TGridModel>(
            IList<TGridModel> Data, int RowIndex, string FieldName)
        {
            string strOtherClassCss = string.Empty;

            Type DataType = Data[RowIndex].GetType();

            IList<PropertyInfo> props = new List<PropertyInfo>(DataType.GetProperties());
            PropertyInfo objPropertyInfo = props.FirstOrDefault(x => x.Name == FieldName);

            if (objPropertyInfo != null)
            {
            }

            return strOtherClassCss;
        }

        public static string RetrieveColumnCustomFormat(PropertyInfo objPropertyInfo, int RowIndex)
        {
            return string.Empty;
        }
    }
}