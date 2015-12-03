using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DapperDawgBll;
using DapperDawgModels;
using DapperDog.Models;

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

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PR")]
        public ActionResult AddBlogPost()
        {
            var ops = new BlogPostOperations();
            var vm = new AddBlogPostViewModel(ops.GetAllCategories());
            return View(vm);  // There is no input for an author. Should we include as an input or us user table?
        }

        public ActionResult PostToRepo(BlogPost blogPost)
        {
            var ops = new BlogPostOperations();

            ops.AddNewBlogPost(blogPost);  

            return RedirectToAction("Index");
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