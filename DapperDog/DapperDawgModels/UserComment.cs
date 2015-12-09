using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDawgModels
{
    public class UserComment
    {
        public int UserCommentID { get; set;} 
        public string UserCommentUserName { get; set; }
        [DataType(DataType.MultilineText)]
        public string UserCommentContent { get; set; }
        public DateTime UserCommentDate { get; set; }
    }
}
