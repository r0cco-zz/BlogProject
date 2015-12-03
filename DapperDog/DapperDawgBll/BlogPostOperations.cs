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
                post.BlogTags = new List<Tag>();
                var tagList = _repo.GetTagsByPostID(post.PostID);
                if (tagList != null)
                {
                    foreach (var tag in tagList)
                    {
                        post.BlogTags.Add(tag);
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
            newBlogPost.PostDate = DateTime.Now;
            newBlogPost.Author = "Author";
            newBlogPost.PostStatus = 1;
            var postId = _repo.AddNewBlogPost(newBlogPost);
            var tagList = _repo.GetAllTags();
            var tagExists = false;
            var tagId = 0;

            foreach (var tag in newBlogPost.tags)
            {
                foreach (var tag2 in tagList)
                {
                    if (tag2.TagName == tag)
                    {
                        // Tag already exists, bool set to true
                        tagExists = true;
                        tagId = tag2.TagID;
                        break;
                    }
                }
                // Check bool to see if tag exists
                if (tagExists)
                {
                    // Tag exists on table, only need to add it to PostTags tot tie it to a post
                    _repo.AddNewPostTag(tagId, postId);
                }
                else
                {
                    // Tag doesn't exist on table, needs to be created, then tied to a post on PostTags table
                    tagId = _repo.AddNewTag(tag);
                    _repo.AddNewPostTag(tagId, postId);
                }
            }
        }
    }
}
