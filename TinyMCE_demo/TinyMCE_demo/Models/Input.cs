using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TinyMCE_demo.Models
{
    public class Input
    {
        [AllowHtml]
        public string InputContent { get; set; }
    }
}