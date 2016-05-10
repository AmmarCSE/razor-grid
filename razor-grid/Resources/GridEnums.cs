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

    /// <summary>
    /// Represents support for the HTML label element in an ASP.NET MVC view.
    /// </summary>
    public static class GridEnums
    {
        #region GridEnums :: Grid Permission
        /// <summary>
        /// The grid permission.
        /// </summary>
        /// <remarks>
        /// Integer enumerator values should be the same as in DataSettings.ActivityType table.
        /// </remarks>
        public enum GridPermission
        {
            /// <summary>
            /// Add operation button.
            /// </summary>
            Add = 2,

            /// <summary>
            /// The edit operation button.
            /// </summary>
            Edit = 3,

            /// <summary>
            /// The delete and delete all operation buttons.
            /// </summary>
            Delete = 4,

            /// <summary>
            /// The activate and deactivate operation buttons.
            /// </summary>
            Update_Activation = 5,

            /// <summary>
            /// The reset password operation button.
            /// </summary>
            ResetPassword = 6,

            /// <summary>
            /// The lock and unlock operation button.
            /// </summary>
            Lock = 7,

            /// <summary>
            /// The print operation button.
            /// </summary>
            Print = 8,

            /// <summary>
            /// The Details button.
            /// </summary>
            Details = 9
        }
        #endregion GridEnums :: Grid Permission

        #region GridEnums :: Column Custom Format

        /// <summary>
        /// The column custom format.
        /// </summary>
        public enum ColumnCustomFormat
        {
            /// <summary>
            /// The price format.
            /// </summary>
            [Description("{0:#0.00}")]
            PriceFormat,

            /// <summary>
            /// The price with 1000 separator.
            /// </summary>
            [Description("{0:#,#0.00}")]
            PriceWith1000Separator,

            /// <summary>
            /// The custom format.
            /// </summary>
            CustomFormat,
        }

        #endregion GridEnums :: Column Custom Format
    }
}