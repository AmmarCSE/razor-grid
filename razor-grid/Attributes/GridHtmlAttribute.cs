using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AmmarCSE.RazorGrid.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class GridHtmlAttribute : Attribute
    {
         public string Attr { get; set; }
         public object AttrVal { get; set; }
    }
}