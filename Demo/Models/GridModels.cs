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
        [Display(Name = "First Name")]
        [GridHtml(Attr = "style", AttrVal = "width:20%")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [GridHtml(Attr = "style", AttrVal = "width:20%")]
        public string LastName { get; set; }

        [Display(Name = "Age")]
        [GridHtml(Attr = "style", AttrVal = "width:13%")]
        public int? Age { get; set; }

        [Display(Name = "Join Date")]
        [GridHtml(Attr = "style", AttrVal = "width:26%")]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Email")]
        [GridHtml(Attr = "style", AttrVal = "width:20%")]
        public string Email { get; set; }
    }

    public class GridModelList
    {
        public List<GridModel> Data { get; set; }
        public List<GridEnums.GridPermission> gridPermissions { get; set; }
        public Dictionary<GridEnums.GridPermission, string> gridActions { get; set; }

        public GridModelList()
        {
            DateTime now = DateTime.Now;
            Data = new List<GridModel>();
            Data.Add(new GridModel() {FirstName = "John", LastName = "Doe", Age = 23, Email = "john@email.com", JoinDate = now.AddDays(-7) });
            Data.Add(new GridModel() {FirstName = "Jane", LastName = "Smith", Age = 34, Email = "jane@email.com", JoinDate = now.AddYears(-7) });
            Data.Add(new GridModel() {FirstName = "Jack", LastName = "Black", Age = 32, Email = "black@email.com", JoinDate = now.AddMonths(-7) });
            Data.Add(new GridModel() {FirstName = "John", LastName = "Public", Age = 46, Email = "johnQ@email.com", JoinDate = now.AddDays(-2) });
            Data.Add(new GridModel() {FirstName = "Even", LastName = "Steven", Age = 26, Email = "evensteven@email.com", JoinDate = now.AddMonths(-3) });
            Data.Add(new GridModel() {FirstName = "Joe", LastName = "Sixpack", Age = 66, Email = "sixpack@email.com", JoinDate = now.AddDays(-77) });

            gridPermissions = new List<GridEnums.GridPermission>();
            gridPermissions.Add(GridEnums.GridPermission.Add);
            gridPermissions.Add(GridEnums.GridPermission.Delete);
            gridPermissions.Add(GridEnums.GridPermission.Update_Activation);
            gridPermissions = new List<GridEnums.GridPermission>();

            gridActions = new Dictionary<GridEnums.GridPermission, string>();
            //gridActions.Add(GridEnums.GridPermission.Add, GridCustomScript.ACTION_REDIRECT("'google.com'"));
        }
    }
}
