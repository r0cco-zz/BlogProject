using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDawgModels
{
    public class BlogPost
    {
        public int PostID { get; set; }
        public int CategoryID { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public string Author { get; set; }
        public int PostStatus { get; set; }
        public List<Tag> Tags { get; set; } 
    }
}
