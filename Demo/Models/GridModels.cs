using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc.Html;
using System.Web.Security;

using AmmarCSE.RazorGrid.Resources;
using AmmarCSE.RazorGrid.Attributes;

namespace Demo.Models
{
    public class GridModel
    {
        [Required]
        [Display(Name = "Name")]
        [GridHtml(Attr = "style", AttrVal = "width:19%")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        [GridHtml(Attr = "style", AttrVal = "width:19%")]
        public string Address { get; set; }

        [Display(Name = "Age")]
        [GridHtml(Attr = "style", AttrVal = "width:19%")]
        public int? Age { get; set; }
    }

    public class GridModelList
    {
        public List<GridModel> Data { get; set; }
        public List<GridEnums.GridPermission> gridPermissions { get; set; }
        public Dictionary<GridEnums.GridPermission, string> gridActions { get; set; }

        public GridModelList()
        {
            Data = new List<GridModel>();
            Data.Add(new GridModel() {Name = "1", Address = "2", Age = 3 });
            Data.Add(new GridModel() {Name = "4", Address = "5", Age = 6 });

            gridPermissions = new List<GridEnums.GridPermission>();
            gridPermissions.Add(GridEnums.GridPermission.Add);
            gridPermissions.Add(GridEnums.GridPermission.Delete);
            gridPermissions.Add(GridEnums.GridPermission.Update_Activation);

            gridActions = new Dictionary<GridEnums.GridPermission, string>();
            //gridActions.Add(GridEnums.GridPermission.Add, GridCustomScript.ACTION_REDIRECT("'google.com'"));
        }
    }
}
