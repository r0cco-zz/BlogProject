using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperDawgBll;
using DapperDawgData;
using DapperDawgModels;
using DapperDog.Models;

namespace DapperDog.Controllers
{
    public class BlogsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PR")]
        public ActionResult AddBlogPost()
        {
            var ops = new BlogPostOperations();
            var vm = new AddBlogPostViewModel(ops.GetAllCategories());
            return View(vm); // There is no input for an author. Should we include as an input or use user table?
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PR")]
        [HttpPost]
        public ActionResult AddBlogPost(BlogPost blogPost)
        {
            var ops = new BlogPostOperations();

            ops.AddNewBlogPost(blogPost);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PR")]
        public ActionResult AddStaticPages()
        {
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PR")]
        [HttpPost]
        public ActionResult AddStaticPages(StaticPage newStaticPage)
        {
            var repo = new BlogPostRepository();
            repo.AddNewStaticPage(newStaticPage);

            return RedirectToAction("Index", "Home");
        }
    }
}