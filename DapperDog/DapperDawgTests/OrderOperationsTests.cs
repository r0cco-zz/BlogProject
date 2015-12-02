using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDawgBll;
using DapperDawgData.Config;
using DapperDawgModels;
using NUnit.Framework;

namespace DapperDawgTests
{
    [TestFixture]
    public class OrderOperationsTests
    {

        private SqlConnection _cn;

        [OneTimeSetUp]
        public void Setup()
        {
            _cn = new SqlConnection(Settings.ConnectionString);
        }

        [Test]
        public void AddnewBlogPostWithNewTag_ShouldReturnNewTagId()
        {
            int expected = 0;
            int newTagId = 0;

            BlogPost newBlogPost = new BlogPost()
            {
                CategoryID = 2,
                CategoryName = "Pet Health",
                PostTitle = "Teeth",
                PostDate = DateTime.Now,
                PostContent = "teeth",
                Author = "Joe Schmoe",
                PostStatus = 0,
                Tags = new List<Tag>
                {
                    new Tag {TagName = "teeth"}
                }
            };

            expected = RetrieveLastTagId() + 1;

            BlogPostOperations bpo = new BlogPostOperations();
            bpo.AddNewBlogPost(newBlogPost);

            newTagId = RetrieveLastTagId();
            Assert.AreEqual(expected, newTagId);
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
