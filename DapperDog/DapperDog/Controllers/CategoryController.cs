using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DapperDawgBll;
using DapperDawgModels;

namespace DapperDog.Controllers
{
    public class CategoryController : ApiController
    {
        public List<Category> Get()
        {
            var ops = new BlogPostOperations();
            return ops.GetAllCategories();
        }

        public HttpResponseMessage Post(Category newCategory)
        {
            var ops = new BlogPostOperations();
            ops.AddCategory(newCategory);

            var response = Request.CreateResponse(HttpStatusCode.Created, newCategory);

            string uri = Url.Link("DefaultApi", new {id = newCategory.CategoryID});
            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}
