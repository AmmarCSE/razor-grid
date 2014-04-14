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
    public static class GridCustomElement
    {
        public static string ADD_BUTTON(string action)
        {
            return string.Format("<button onclick=\"{0}\">Add new</button>", action);
        }
        public static string DELETE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">D</button>"; 
        public static string ACTIVATE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">A</button>"; 
        public static string UNACTIVATE_ICON = "<button style=\"background-color: rgb(238, 238, 238);\">U</button>"; 
        public static string PAGER = "<div><button>L</button><button>P</button><button>N</button><button>L</button></div>"; 
    }

    public static class GridCustomScript
    {
        public static string ACTION_REDIRECT(string Url)
        {
            return string.Format("GridActionRedirect({0})", Url);
        }
    }
}