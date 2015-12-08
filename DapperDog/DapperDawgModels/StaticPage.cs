using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DapperDawgModels
{
     public class StaticPage
    {
        public int StaticPageID { get; set; }
        public DateTime StaticPageDate { get; set; }
        public string StaticPageTitle { get; set; }
        [AllowHtml]
        public string StaticPageContent { get; set; }
    }
}
