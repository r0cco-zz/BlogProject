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
    public class TagController : ApiController
    {
        public List<Tag> Get()
        {
            var ops = new BlogPostOperations();
            return ops.GetAllTags();
        } 
    }
}
