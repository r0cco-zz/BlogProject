using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperDawgModels;

namespace DapperDog.Models
{
    public class AddBlogPostViewModel
    {
        public List<SelectListItem> Categories { get; set; }

        public AddBlogPostViewModel(List<Category> categoryList)
        {
            Categories = new List<SelectListItem>();

            foreach (var category in categoryList)
            {
                var selectcategory = new SelectListItem();

                selectcategory.Text = category.CategoryName;
                selectcategory.Value = category.CategoryID.ToString();

                Categories.Add(selectcategory);
            }
        } 
    }
}