using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperDawgModels;

namespace DapperDog.Models
{
    public class ListSinglePostViewModel
    {
        public BlogPost BlogPost { get; set; }
        public List<Category> Categories { get; set; }
        public List<StaticPage> StaticPages { get; set; }
        //public int PostTotal { get; set; }
        public int RouteID { get; set; } 
    }
}