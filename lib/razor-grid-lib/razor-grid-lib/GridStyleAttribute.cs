using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace razor_grid_lib
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=true)]
    public class GridHtmlAttribute : Attribute
    {
         public string Attr { get; set; }
         public object AttrVal { get; set; }
    }
}