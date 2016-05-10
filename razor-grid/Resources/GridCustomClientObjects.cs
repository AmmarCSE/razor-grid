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
    public static class GridCustomElement
    {
        public static string Empty_Div = "<span\"></span>";
        public static string ADD_BUTTON = "<button type=\"button\">Add new</button>";
        public static string DELETE_ICON = "<span title=\"Delete\" class=\"icon-trash\"></span>Delete"; 
        public static string EDIT_ICON = "<div class=\"actionColumnContainer\"><span title=\"Edit\" class=\"icon-edit editRow\"></span><span class=\"icon-text\">Edit</span></div>";
        public static string DETAILS_ICON = "<span title=\"Details\" class=\"icon-list rowDetails\"></span>";
        public static string ACTIVATE_ICON = "<span title=\"Activate\" class=\"icon-ok-circle\"></span>Activate";
        public static string UNACTIVATE_ICON = "<span title=\"Deactivate\" class=\"icon-ban-circle\"></span>Deactivate";
        public static string LOCK_ICON = "<span title=\"Lock\" class=\"icon-lock\"></span>";
        public static string UNLOCK_ICON = "<span title=\"Unlock\" class=\"icon-unlock\"></span>";
        public static string RESETPASSWORD_ICON = "<span title=\"Reset Password\" class=\"icon-pencil\"></span>"; 
        // public static string PAGER = "<div class=\"gridPager\"><button type=\"button\" class=\"firstPage\">First</button><button type=\"button\" class=\"previousPage\">Previous</button><button type=\"button\" class=\"nextPage\">Next</button><button type=\"button\" class=\"lastPage\">Last</button></div>"; 
        public static string PAGER_BUTTON_PREV = "<button type=\"button\" class=\"previous\" disabled=\"disabled\"><</button>";
        public static string PAGER_BUTTON_NEXT = "<button type=\"button\" data-page-index=\"1\" {0} class=\"next\">></button>";
    }
}