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
        /// <summary>
        /// The extract grid model properties.
        /// </summary>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public static List<PropertyInfo> ExtractGridModelProperties<TGridModel>()
        {
            return new List<PropertyInfo>(typeof(TGridModel).GetProperties());
        }

        /// <summary>
        /// The extract grid model select properties.
        /// </summary>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public static List<PropertyInfo> ExtractGridModelSelectProperties<TGridModel>()
        {
            return
                new List<PropertyInfo>(
                    typeof(TGridModel).GetProperties());
        }

        /// <summary>
        /// The extract key properties.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The IList.
        /// </returns>
        public static IList<PropertyInfo> ExtractKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        /// <summary>
        /// The extract non key properties.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The IList.
        /// </returns>
        public static IList<PropertyInfo> ExtractNonKeyProperties(IList<PropertyInfo> properties)
        {
            return properties = properties.Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute), false)).ToList();
        }

        /// <summary>
        /// The extract quick search properties.
        /// </summary>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
        public static List<PropertyInfo> ExtractQuickSearchProperties<TModel>()
        {
            return null;// typeof(TModel).GetProperties().Where(p => Attribute.IsDefined(p, typeof(QuickSearchAttribute), false)).ToList();
        }

        /// <summary>
        /// The retrieve html attributes.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The Dictionary.
        /// </returns>
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

        /// <summary>
        /// The retrieve parent html attributes.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The Dictionary.
        /// </returns>
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

        /// <summary>
        /// The retrieve action attributes.
        /// </summary>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The Dictionary.
        /// </returns>
        public static Dictionary<string, object> RetrieveActionAttributes<TGridModel>()
        {
            GridActionAttribute[] attributes =
                (GridActionAttribute[])typeof(TGridModel).GetCustomAttributes(typeof(GridActionAttribute), false);

            return attributes.ToDictionary(attribute => attribute.Action, attribute => attribute.ActionVal);
        }

        /// <summary>
        /// The retrieve grid entity property path.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The dynamic.
        /// </returns>
        public static dynamic RetrieveGridEntityPropertyPath(PropertyInfo property)
        {
            return null;
        }

        /// <summary>
        /// The determine type code.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int DetermineTypeCode(PropertyInfo property)
        {
            bool isNullable; // dummy variable
            return DetermineTypeCodeAndNullability(property, out isNullable);
        }

        /// <summary>
        /// The determine type code and nullability.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="isNullable">
        /// The is nullable.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int DetermineTypeCodeAndNullability(PropertyInfo property, out bool isNullable)
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

            if (typeCode == TypeCode.Object && property.PropertyType.Name == "TimeSpan")
            {
                return 17;
            }

            return (int)typeCode;
        }

        /// <summary>
        /// The get extra css class.
        /// </summary>
        /// <param name="Data">
        /// The data.
        /// </param>
        /// <param name="RowIndex">
        /// The row index.
        /// </param>
        /// <param name="FieldName">
        /// The Field Name.
        /// </param>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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

        /// <summary>
        /// The retrieve column custom format.
        /// </summary>
        /// <param name="objPropertyInfo">
        /// The obj Property Info.
        /// </param>
        /// <param name="RowIndex">
        /// The row index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RetrieveColumnCustomFormat(PropertyInfo objPropertyInfo, int RowIndex)
        {
            return string.Empty;
        }
    }
}