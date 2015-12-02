using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDawgData;
using DapperDawgModels;

namespace DapperDawgBll
{
    public class BlogPostOperations
    {
        private readonly BlogPostRepository _repo;

        public BlogPostOperations()
        {
            _repo = new BlogPostRepository();
        }

        public List<BlogPost> GetBlogPosts()
        {
            var posts = _repo.GetAllBlogPosts();

            foreach (var post in posts)
            {
                post.CategoryName = _repo.GetCategoryByPostID(post.PostID);
                post.Tags = new List<Tag>();
                var tagList = _repo.GetTagsByPostID(post.PostID);
                if (tagList != null)
                {
                    foreach (var tag in tagList)
                    {
                        post.Tags.Add(tag);
                    }
                }
            }
            return posts;
        }

        public List<Category> GetAllCategories()
        {
            return _repo.GetAllCategories();
        }

        public void AddNewBlogPost(BlogPost newBlogPost)
        {
            _repo.AddNewBlogPost(newBlogPost);
            var tagList = _repo.GetAllTags();

            foreach (var tag in newBlogPost.Tags)
            {
                if (tagList.Contains(tag))
                {
                    _repo.AddNewPostTag(newBlogPost.PostID, tag.TagID);
                }
                else
                {
                  int tagId =  _repo.AddNewTag(tag.TagName);
                    _repo.AddNewPostTag(newBlogPost.PostID, tagId);
                }   
            }
        }
    }
}
