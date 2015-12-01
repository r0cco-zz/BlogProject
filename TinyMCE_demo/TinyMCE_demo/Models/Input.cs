using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TinyMCE_demo.Models
{
    public class Input
    {
        // allowhtml attribute lets html in a string without .net flaggin it as unsafe (without it, the page would throw an exception).
        [AllowHtml]
        public string InputContent { get; set; }
    }
}