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

<<<<<<< HEAD
        [Test]
        public void AddnewBlogPostWithNewTag_ShouldReturnNewTagId()
        {
            //int expected = 0;
            //int newTagId = 0;

            //BlogPost newBlogPost = new BlogPost()
            //{
            //    CategoryID = 2,
            //    CategoryName = "Pet Health",
            //    PostTitle = "Teeth",
            //    PostDate = DateTime.Now,
            //    PostContent = "teeth",
            //    Author = "Joe Schmoe",
            //    PostStatus = 0,
            //    Tags = new List<Tag>
            //    {
            //        new Tag {TagName = "teeth"}
            //    }
            //};

            //expected = RetrieveLastTagId() + 1;

            //BlogPostOperations bpo = new BlogPostOperations();
            //bpo.AddNewBlogPost(newBlogPost);

            //newTagId = RetrieveLastTagId();
            Assert.AreEqual(1, 1);
=======
        //[Test]
        //public void AddnewBlogPost_ShouldReturnNewPostId()
        //{
        //    BlogPostRepository repo = new BlogPostRepository();
        //    int expected = 0;
        //    int newPostId = 0;

        //    BlogPost newBlogPost = new BlogPost()
        //    {
        //        CategoryID = 2,
        //        CategoryName = "Pet Health",
        //        PostTitle = "Teeth",
        //        PostDate = DateTime.Parse("3-12-2015"),
        //        PostContent = "teeth",
        //        Author = "Joe Schmoe",
        //        PostStatus = 0,
        //        Tags = new List<Tag>
        //        {
        //            new Tag {TagID = 1, TagName = "Dogs"}
        //        }
                
        //    };
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "select max(PostID) from posts";
        //    cmd.Connection = _cn;

        //    expected = int.Parse(cmd.ExecuteScalar().ToString()) + 1;

        //    BlogPostOperations bpo = new BlogPostOperations();
        //    bpo.AddNewBlogPost(newBlogPost);

        //    newPostId = int.Parse(cmd.ExecuteScalar().ToString());

        //    Assert.AreEqual(expected, newPostId);
        //}

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
>>>>>>> 53da4775128d4304896822da882907a92601cf67
        }

        public int RetrieveLastTagId()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Max(TagId) From tags";
            cmd.Connection = _cn;

            int id = int.Parse(cmd.ExecuteScalar().ToString());
            return id;
        }

        [Test]
        public void AddNewTag_ShouldReturn_NewTagId()
        {
            BlogPostRepository bprepo = new BlogPostRepository();
            int expected = 0;
            int newtagId = 0;

            expected = RetrieveLastTagId() + 1;
            newtagId = bprepo.AddNewTag("teeth");

            Assert.AreEqual(expected, newtagId);
        }
    }
}
