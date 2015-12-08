using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DapperDawgModels
{
    public class BlogPost
    {
        public int PostID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDate { get; set; }
        [AllowHtml]
        public string PostContent { get; set; }
        public bool IsStickyPost { get; set; }
        public string Author { get; set; }
        public int PostStatus { get; set; }
        public List<Tag> BlogTags { get; set; }
        public List<string> tags { get; set; } 
        public DateTime? PublishDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
