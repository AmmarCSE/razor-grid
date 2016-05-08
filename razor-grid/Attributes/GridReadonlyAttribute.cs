using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AmmarCSE.RazorGrid.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GridReadonlyAttribute : Attribute
    {
    }
}