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
            var vm = new HomeIndexViewModel();
            var ops = new BlogPostOperations();
            vm.BlogPosts = ops.GetBlogPosts();
            vm.Categories = ops.GetAllCategories();

            return View(vm);
        }

        public ActionResult ListPostsByCategory(int id)
        {
            var ops = new BlogPostOperations();
            var vm = new HomeIndexViewModel();
            vm.BlogPosts = ops.GetPostsByCategoryID(id);
            vm.Categories = ops.GetAllCategories();

            return View("Index", vm);
        }

        

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}