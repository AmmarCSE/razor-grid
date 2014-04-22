using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

namespace Travel.Agency.RazorGrid.GridResources
{
    // Summary:
    //     Represents support for the HTML label element in an ASP.NET MVC view.
    public static class GridEnums
    {
        public enum GridPermission
        {
            Add = 1,
            Edit = 2,
            Delete = 3,
            Update_Activation = 4
        }
    }
}