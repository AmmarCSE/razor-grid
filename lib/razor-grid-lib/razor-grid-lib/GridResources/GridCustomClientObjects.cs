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
    public static class GridCustomElement
    {
        public static string ADD_BUTTON = "<button>Add new</button>";
        public static string DELETE_ICON = "<span title=\"Delete\" class=\"icon-trash deleteRow\"></span>"; 
        public static string EDIT_ICON = "<span title=\"Edit\" class=\"icon-edit editRow\"></span>"; 
        public static string ACTIVATE_ICON = "<span title=\"Activate\" class=\"icon-ok-circle\"></span>"; 
        public static string UNACTIVATE_ICON = "<span title=\"Unactivate\" class=\"icon-ban-circle\"></span>"; 
        public static string PAGER = "<div class=\"gridPager\"><button class=\"firstPage\">First</button><button class=\"previousPage\">Previous</button><button class=\"nextPage\">Next</button><button class=\"lastPage\">Last</button></div>"; 
    }
}