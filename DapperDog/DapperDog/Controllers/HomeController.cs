using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperDawgBll;

namespace DapperDog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ops = new BlogPostOperations();
            var posts = ops.GetBlogPosts();
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}