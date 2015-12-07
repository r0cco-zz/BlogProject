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

        public List<StaticPage> GetAllStaticPages()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<StaticPage>("GetAllStaticPages", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public StaticPage GetStaticPageByID(int id)
        {
            var p = new DynamicParameters();
            p.Add("StaticPageID", id);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<StaticPage>("GetStaticPageByID", p, commandType:CommandType.StoredProcedure).FirstOrDefault();
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
            p.Add("IsStickyPost", newBlogPost.IsStickyPost);
            p.Add("PostID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _cn.Execute("AddNewPost", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("PostID");
        }

        public void EditBlogPost(BlogPost editedPost)
        {
            // Different stored procedures for updating each field. This way it retains the same PostID

            var p = new DynamicParameters();
            p.Add("postId", editedPost.PostID);
            p.Add("newCategoryId", editedPost.CategoryID);
            _cn.Execute("UpdateCategoryID", p, commandType: CommandType.StoredProcedure);

            var p2 = new DynamicParameters();
            p2.Add("postId", editedPost.PostID);
            p2.Add("newPostTitle", editedPost.PostTitle);
            _cn.Execute("UpdatePostTitle", p2, commandType: CommandType.StoredProcedure);

            //p.Add("newPostDate", editedPost.PostDate); no option to edit date yet

            var p3 = new DynamicParameters();
            p3.Add("postId", editedPost.PostID);
            p3.Add("newPostContent", editedPost.PostContent);
            _cn.Execute("UpdatePostContent", p3, commandType: CommandType.StoredProcedure);

            //p.Add("newAuthor", editedPost.Author); no option to change author yet
            //p.Add(newIsStickyPost, editedPost.IsStickyPost); no option to edit IsStickyPost yet

            var p5 = new DynamicParameters();
            p5.Add("postId", editedPost.PostID);
            p5.Add("newIsStickyPost", editedPost.IsStickyPost);
            _cn.Execute("UpdateIsStickyPost", p5, commandType: CommandType.StoredProcedure);
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

        public void RemovePostFromPostTagsTable(int id)
        {
            var p = new DynamicParameters();
            p.Add("postId", id);

            _cn.Execute("RemovePostFromPostTagsTable", p, commandType: CommandType.StoredProcedure);
        }

        public void ApproveBlogPost(int postId)
        {
            var p = new DynamicParameters();
            p.Add("postId", postId);

            _cn.Execute("SetPostStatusTo1", p, commandType: CommandType.StoredProcedure);
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

        public void AddNewCategory(Category newCategory)
        {
            var p = new DynamicParameters();
            p.Add("categoryName", newCategory.CategoryName);

            _cn.Execute("AddNewCategory", p, commandType: CommandType.StoredProcedure);
        }

    }
}
