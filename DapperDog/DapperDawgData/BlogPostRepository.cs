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
        public List<BlogPost> GetAllBlogPosts()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var blogPosts = cn.Query<BlogPost>("GetAllPostsOrderedByDate", commandType: CommandType.StoredProcedure).ToList();

                return blogPosts;
            }
        }

        public BlogPost GetBlogPostByID(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("postId", id);

                var post = cn.Query<BlogPost>("GetPostByID", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return post;
            }
        }

        public List<Tag> GetAllTags()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var tags = cn.Query<Tag>("GetAllTags", commandType: CommandType.StoredProcedure).ToList();

                return tags;
            }
        }

        public List<Tag> GetTagsByPostID(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("postId", id);

                var tags = cn.Query<Tag>("GetAllTagsOnAPostByPostID", p, commandType: CommandType.StoredProcedure).ToList();

                return tags;
            }
        }   
    }
}
