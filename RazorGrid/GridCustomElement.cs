using RazorGrid.Models;
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
    public static class GridCustomElement
    {
        public static string ADD_BUTTON = "<a  href=\"/DataSettings/Hotels/HotelEditor?HotelID=0\" data-original-title=\"Add New\">Add new</a>"; 
        public static string DELETE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">D</button>"; 
        public static string ACTIVATE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">A</button>"; 
        public static string UNACTIVATE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">U</button>"; 
    }
}