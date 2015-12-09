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
        public ActionResult Index(int id = 0)
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
            //vm.BlogPosts = ops.GetBlogPosts();
            vm.Categories = ops.GetAllCategories();
            vm.StaticPages = ops.GetAllStaticPages();
            // paging
            var posts = ops.GetBlogPosts();
            vm.PostTotal = posts.Count;
            vm.RouteID = id;
            vm.BlogPosts = posts.Skip(id*5).Take(5).ToList();

            return View(vm);
        }

        public ActionResult ListPostsByCategory(int id)
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
            vm.BlogPosts = ops.GetPostsByCategoryID(id);
            vm.Categories = ops.GetAllCategories();
            vm.StaticPages = ops.GetAllStaticPages();

            return View("Index", vm);
        }

        public ActionResult DisplayStaticPage(int id)
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
           var newStaticPage = ops.GetStaticPageByID(id);

            return View(newStaticPage);
        }

        public ActionResult ListPostsByTag(int id)
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
            vm.BlogPosts = ops.GetPostsByTagID(id);
            vm.Categories = ops.GetAllCategories();
            vm.StaticPages = ops.GetAllStaticPages();

            return View("Index", vm);
        }

        public ActionResult ListSinglePost(int id)
        {
            // Added a new view in order to view the whole post without the truncation from dotdotdot.
            // Also for viewing comments
            var ops = new BlogPostOperations();
            //var vm = new HomeIndexViewModel();
            var vm = new ListSinglePostViewModel();
            vm.BlogPost = ops.GetPostByID(id).FirstOrDefault();
            vm.Categories = ops.GetAllCategories();
            vm.StaticPages = ops.GetAllStaticPages();
            //vm.RouteID = ; can we use this to go back to the right routed page on the home index?

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddUserComment(AddUserCommentViewModel vm)
        {
            var ops= new BlogPostOperations();
            var userComment = new UserComment();
            userComment.UserCommentContent = vm.UserCommentContent;
            userComment.UserCommentDate = DateTime.Parse(vm.UserCommentDate);
            userComment.UserCommentUserName = vm.UserCommentUserName;
            

            ops.AddNewUserComment(userComment, vm.PostID);
            
            return RedirectToAction("ListSinglePost", vm.PostID);
        }
    }
}