using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperDawgModels;

namespace DapperDog.Models
{
    public class EditPostViewModel
    {
        public List<SelectListItem> Categories { get; set; }
        public BlogPost BlogPost { get; set; }

        public EditPostViewModel (List<Category> categoryList)
        {
            Categories = new List<SelectListItem>();

            foreach (var category in categoryList)
            {
                var selectcategory = new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryID.ToString()
                };


                Categories.Add(selectcategory);
            }
        }
    }
}