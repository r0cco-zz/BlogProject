using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DapperDawgModels;

namespace DapperDog.Models
{
    public class AddUserCommentViewModel
    {
        public string UserCommentUserName { get; set; }
        public string UserCommentContent { get; set; }
        public string UserCommentDate { get; set; }
        public int PostID { get; set; }
    }
}