using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperDawgData.Config;
using DapperDawgModels;

namespace DapperDawgData
{
    public class BlogPostRepository
    {
        private SqlConnection _cn;

        public BlogPostRepository()
        {
            _cn = new SqlConnection(Settings.ConnectionString);
        }

        public List<BlogPost> GetAllBlogPosts()
        {
            var blogPosts =
                _cn.Query<BlogPost>("GetAllPostsOrderedByDate", commandType: CommandType.StoredProcedure).ToList();

            return blogPosts;
        }

        public BlogPost GetBlogPostByID(int id)
        {
            var p = new DynamicParameters();
            p.Add("postId", id);

            var post = _cn.Query<BlogPost>("GetPostByID", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return post;
        }

        public List<Tag> GetAllTags()
        {
            var tags = _cn.Query<Tag>("GetAllTags", commandType: CommandType.StoredProcedure).ToList();

            return tags;
        }

        public List<Tag> GetTagsByPostID(int id)
        {
            var p = new DynamicParameters();
            p.Add("postId", id);

            var tags = _cn.Query<Tag>("GetAllTagsOnAPostByPostID", p, commandType: CommandType.StoredProcedure).ToList();

            return tags;
        }

        public List<Category> GetAllCategories()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Category>("SELECT c.CategoryID, c.CategoryName FROM Categories c").ToList();
            }
        }

        public string GetCategoryByPostID(int id)
        {
            var p = new DynamicParameters();
            p.Add("postId", id);

            var catname =
                _cn.Query<string>("GetCategoryByPostID", p, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

            return catname;
        }

        public void AddNewBlogPost(BlogPost newBlogPost)
        {
            var p = new DynamicParameters();
            p.Add("CategoryID", newBlogPost.CategoryID);
            p.Add("PostTitle", newBlogPost.PostTitle);
            p.Add("PostDate", newBlogPost.PostDate, dbType:DbType.Date, direction:ParameterDirection.Input);
            p.Add("PostContent", newBlogPost.PostContent);
            p.Add("Author", newBlogPost.Author);
            p.Add("PostStatus", newBlogPost.PostStatus);

            p.Add("postId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _cn.Execute("AddNewPost", p, commandType: CommandType.StoredProcedure);
            int newPostId = p.Get<int>("TagId");

            p.Add("PostID", p, dbType:DbType.Int32, direction:ParameterDirection.Output);

            _cn.Execute("AddNewPost", p, commandType: CommandType.StoredProcedure);
            int newPostID = p.Get<int>("PostID");

        }

        public void AddNewPostTag(int PostId, int TagId)
        {
            var p = new DynamicParameters();
            p.Add("PostId", PostId);
            p.Add("TagId", TagId);

            _cn.Execute("AddNewPostsTags", p, commandType: CommandType.StoredProcedure);
        }

        public int AddNewTag(string TagName)
        {
            var p = new DynamicParameters();
            p.Add("TagName", TagName);

            //p.Add("tagId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            p.Add("TagId", dbType: DbType.Int32, direction: ParameterDirection.Output);


            _cn.Execute("AddNewTag", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("TagId");
        }
    }
}
