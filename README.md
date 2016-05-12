razor-grid | Html Helper For Editable Grid
====================================================
![alt tag](https://raw.github.com/AmmarCSE/razor-grid/master/Preview.png)

razor-grid is an ASP.NET Razor extension for generating an HTML grid. The extension is made to work with any model type and handles different property types. The grid engine is geared to *smartly* handle different types such as displaying raw ```DateTime``` variables in string format or putting property values specified with the ```[Key]``` attribute in hidden fields. 

Getting Set Up
--------------


1 - Include ```razor-grid``` as a reference in your project.

2 - Specify your data model with desired attributes:
```
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

```

3 - Invoke the extension helper method with the list of data:
```
@Html.GridFor(m => m.Data)
```

ToDo
--------------
Convert markup to HTML tables since the grid is *tabular* data

Clean/refactor css implementation

Format and place comments in code.
