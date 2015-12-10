using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDawgBll;
using DapperDawgData;
using DapperDawgData.Config;
using DapperDawgModels;
using NUnit.Framework;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DapperDawgTests
{
    [TestFixture]
    public class OrderOperationsTests
    {

        public string TestSetupConnectionString;

        public string TestConnectionString =
            @"Server=MS-STDN-012\SQL2014;User=sa;Password=sqlserver;Database=TestPetShopBlog;";

        public BlogPostRepository repo = new BlogPostRepository();

        [TestFixtureSetUp]
        public void Init()
        {
            //TestConnectionString = @"Server=MS-STDN-012\SQL2014;User=sa;Password=sqlserver;Database=TestPetShopBlog;";

            TestSetupConnectionString = ConfigurationManager.ConnectionStrings["Setup"].ConnectionString;


            using (SqlConnection cn = new SqlConnection(TestSetupConnectionString))
            {

                string scriptLoc = @"C:\_repos\BlogProject\DapperDog\DapperDawgTests\SqlTests\dbsetup.sql";

                string script = File.ReadAllText(scriptLoc);

                Server server = new Server(new ServerConnection(cn));

                server.ConnectionContext.ExecuteNonQuery(script);

            }
        }
        //if you run all unit tests it will run them in alphabetical order NOT in the order they are listed here.So this may affect a count

        [TestCase(3,2)]
        public void GetBlogPostsByTag(int tagID, int expected)
        {
            var result = repo.GetBlogPostsByTag(tagID).Count;

            Assert.AreEqual(result, expected);

        }

        [TestCase(2, 3)]
        public void GetBlogPostsByCategory(int categoryID, int expected)
        {
            var result = repo.GetBlogPostsByCategory(categoryID).Count;

            Assert.AreEqual(result, expected);
        }

        [TestCase(4, "Another Title")]
        public void GetBlogPostByID_ShouldReturnPostTitle(int postID, string expected)
        {
            var result = repo.GetBlogPostByID(postID).PostTitle;

            Assert.AreEqual(result, expected);
        }

        [TestCase(5, "toys")]
        public void GetTagsByPostID_ShouldReturnFirstTag(int postID, string expected)
        {
            var result = repo.GetTagsByPostID(postID).FirstOrDefault().TagName;

            Assert.AreEqual(result, expected);
        }

        [TestCase(2, "Contact Us")]
        public void GetStaticPageById_ShouldReturnPageTitle(int staticPageID, string expected)
        {
            var result = repo.GetStaticPageByID(staticPageID).StaticPageTitle;

            Assert.AreEqual(result, expected);
        }

        //Adding a new tag to table returns a tagID which should be 1 higher than last one
        [TestCase("altoids", 17)]
        public void AddNewTagToTable(string tagName, int expected)
        { 
            var result = repo.AddNewTag(tagName);

            Assert.AreEqual(result, expected);
        }

    }


}
