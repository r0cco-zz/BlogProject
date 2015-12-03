using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult AddBlogPost()
        {
            var ops = new BlogPostOperations();
            var vm = new AddBlogPostViewModel(ops.GetAllCategories());
            return View(vm);
        }

        public ActionResult PostToRepo(BlogPost blogPost)
        {
            var ops = new BlogPostOperations();
            blogPost.PostDate = DateTime.Now;
            blogPost.Author = "Author";
            blogPost.PostStatus = 1;
            ops.AddNewBlogPost(blogPost);
            //var tags = Request.Form("myTags li");

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