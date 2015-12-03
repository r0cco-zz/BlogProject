using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDawgBll;
using DapperDawgData;
using DapperDawgData.Config;
using DapperDawgModels;
using NUnit.Framework;

namespace DapperDawgTests
{
    [TestFixture]
    public class OrderOperationsTests
    {

        private SqlConnection _cn;

        [TestFixtureSetUp]
        public void Setup()
        {
            _cn = new SqlConnection(Settings.ConnectionString);
            _cn.Open();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _cn.Dispose();
        }

        [Test]
        public void AddnewBlogPost_ShouldReturnNewPostId()
        {
            int expected;
      
        

            BlogPost newBlogPost = new BlogPost()
            {
                CategoryID = 2,
                CategoryName = "Pet Health",
                PostTitle = "Teeth",
                PostDate = DateTime.Parse("3-12-2015"),
                PostContent = "teeth",
                Author = "Joe Schmoe",
                PostStatus = 0,
                BlogTags = new List<Tag>
                {
                    new Tag {TagID = 1, TagName = "Dogs"}
                }

            };
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select max(PostID) from posts";
            cmd.Connection = _cn;

            expected = int.Parse(cmd.ExecuteScalar().ToString()) + 1;

            BlogPostOperations bpo = new BlogPostOperations();
            bpo.AddNewBlogPost(newBlogPost);

            Assert.AreEqual(expected, newBlogPost.PostID);
        }

        [Test]
        public void CheckNewTagID()
        {
            BlogPostRepository repo = new BlogPostRepository();
            int expected = RetrieveLastTagId() + 1;
            int id = repo.AddNewTag("loopy");
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void CheckGetAllCategories()
        {
            BlogPostRepository repo = new BlogPostRepository();
            int expected = 0;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select max(categoryid) from categories";
            cmd.Connection = _cn;
            expected = int.Parse(cmd.ExecuteScalar().ToString());
            result = repo.GetAllCategories().Count;
            Assert.AreEqual(expected,result);

        }

        public int RetrieveLastTagId()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Max(TagId) From tags";
            cmd.Connection = _cn;

            int id = int.Parse(cmd.ExecuteScalar().ToString());
            return id;
        }

       
    }
}
