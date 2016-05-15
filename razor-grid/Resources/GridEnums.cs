using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

namespace AmmarCSE.RazorGrid.Resources
{
    using System.ComponentModel;

    public static class GridEnums
    {
        public enum GridPermission
        {
            Add = 2,
            Edit = 3,
            Delete = 4,
            Update_Activation = 5,
            ResetPassword = 6,
            Lock = 7,
            Print = 8,
            Details = 9
        }

        public enum ColumnCustomFormat
        {
            [Description("{0:#0.00}")]
            PriceFormat,

            [Description("{0:#,#0.00}")]
            PriceWith1000Separator,
            CustomFormat,
        }
    }
}