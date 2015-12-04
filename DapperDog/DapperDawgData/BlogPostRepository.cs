using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        public List<BlogPost> GetBlogPostsByPR()
        {
            var blogPosts =
                _cn.Query<BlogPost>("GetPostsByPR", commandType: CommandType.StoredProcedure).ToList();

            return blogPosts;
        }

        public List<BlogPost> GetBlogPostsByTag(int tagID)
        {
            var p = new DynamicParameters();
            p.Add("tagId", tagID);
            var blogPosts =
                _cn.Query<BlogPost>("GetPostsByTag", p, commandType: CommandType.StoredProcedure).ToList();

            return blogPosts;
        }

        public List<BlogPost> GetBlogPostsByCategory(int categoryID)
        {
            var p = new DynamicParameters();
            p.Add("categoryID", categoryID);
            var blogPosts =
                _cn.Query<BlogPost>("GetPostsByCategory", p, commandType: CommandType.StoredProcedure).ToList();

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

        public int AddNewBlogPost(BlogPost newBlogPost)
        {
            var p = new DynamicParameters();
            p.Add("CategoryID", newBlogPost.CategoryID);
            p.Add("PostTitle", newBlogPost.PostTitle);
            p.Add("PostDate", newBlogPost.PostDate);
            p.Add("PostContent", newBlogPost.PostContent);
            p.Add("Author", newBlogPost.Author);
            p.Add("PostStatus", newBlogPost.PostStatus);
            p.Add("PostID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _cn.Execute("AddNewPost", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("PostID");
        }

        public void AddNewPostTag(int TagId, int PostId)
        {
            var p = new DynamicParameters();
            p.Add("TagId", TagId);
            p.Add("PostId", PostId);

            _cn.Execute("AddNewPostTags", p, commandType: CommandType.StoredProcedure);
        }

        public int AddNewTag(string TagName)
        {
            var p = new DynamicParameters();
            p.Add("TagName", TagName.ToLower());
            p.Add("TagId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _cn.Execute("AddNewTag", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("TagId");
        }

        public void AddNewStaticPage(StaticPage newStaticPage)
        {
            var p = new DynamicParameters();
            p.Add("StaticPageDate", newStaticPage.StaticPageDate);
            p.Add("StaticPageTitle", newStaticPage.StaticPageTitle);
            p.Add("StaticPageContent", newStaticPage.StaticPageContent);
            p.Add("StaticPageID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _cn.Execute("AddNewStaticPage", p, commandType: CommandType.StoredProcedure);
            p.Get<int>("StaticPageID");
        }


    }
}
