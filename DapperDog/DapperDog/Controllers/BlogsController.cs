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

        [Authorize(Roles = "Admin,PR")]
        //[Authorize(Roles = "PR")]
        public ActionResult AddBlogPost()
        {
            var ops = new BlogPostOperations();
            var vm = new AddBlogPostViewModel(ops.GetAllCategories());
            return View(vm); // Author is currently being populated as the user identity username
        }

        [Authorize(Roles = "Admin,PR")]
        //[Authorize(Roles = "PR")]
        [HttpPost]
        public ActionResult AddBlogPost(BlogPost blogPost)
        {
            var ops = new BlogPostOperations();
            if (User.IsInRole("Admin"))
            {
                // Admin add post, post goes to table with status of 1
                ops.AddNewBlogPost(blogPost);
                return RedirectToAction("Index", "Home");
            }
            if (User.IsInRole("PR"))
            {
                // PR add post, post goes to table with status of 0
                ops.PRAddNewBlogPost(blogPost);
                return RedirectToAction("Index", "Home");
            }

            // somehow a non-authenticated add. nothing goes to data. (necessary?)
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin,PR")]
        //[Authorize(Roles = "PR")]
        public ActionResult AddStaticPages()
        {
            
            return View();
        }

        [Authorize(Roles = "Admin,PR")]
        //[Authorize(Roles = "PR")]
        [HttpPost]
        public ActionResult AddStaticPages(StaticPage newStaticPage)
        {
            var repo = new BlogPostRepository();
            repo.AddNewStaticPage(newStaticPage);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DisplayPostsWithStatus0()
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
            vm.BlogPosts = ops.GetPostsWithStatus0();
            vm.Categories = ops.GetAllCategories();
            vm.StaticPages = ops.GetAllStaticPages();

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(int id)
        {
            var ops = new BlogPostOperations();
            var vm = new EditPostViewModel(ops.GetAllCategories());
            vm.BlogPost = ops.GetPostByID(id).FirstOrDefault();

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditPost(BlogPost editedPost)
        {
            var ops = new BlogPostOperations();
            ops.EditPost(editedPost);

            return RedirectToAction("Index", "Home");
        }
    }
}