using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AmmarCSE.RazorGrid.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class GridActionAttribute : Attribute
    {
         public string Action { get; set; }
         public object ActionVal { get; set; }
    }
}