using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Repository
{
	public class PostRepository:IPostRepository
	{
        private readonly TheBlogDbContext _dbcontext;

        public PostRepository(TheBlogDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ICollection<Post> GetAll()
        {
            return _dbcontext.Posts.Include(a => a.Author).OrderBy(p => p.Id).ToList();
        }

        public Post GetBySlug(string slug)
        {
            return _dbcontext.Posts.Include(a => a.Author).FirstOrDefault(p => p.Path == slug);
        }

        public bool CreatePost(AddPostDTO createPostRequest)
        {
            var post = new Post()
            {
                Title = createPostRequest.Title,
                Content = createPostRequest.Content,
                Summary = createPostRequest.Summary,
                Path = createPostRequest.Path,
                Image = createPostRequest.Image,
                Like = createPostRequest.Like,
                Visible = createPostRequest.Visible,
                UpdatedDate = DateTime.Now,
                PublishDate = DateTime.Now
            };

            post.Id = Guid.NewGuid();
            post.Author = _dbcontext.Users.Find(createPostRequest.AuthorId);
            _dbcontext.Posts.Add(post);
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }

        public bool UpdatePost(Guid postId, EditPostDTO updatePostRequest)
        {
            var existingPost = _dbcontext.Posts.Find(postId);

            if (existingPost == null) return false;

            existingPost.Title = updatePostRequest.Title;
            existingPost.Content = updatePostRequest.Content;
            existingPost.Summary = updatePostRequest.Summary;
            existingPost.Path = updatePostRequest.Path;
            existingPost.Image = updatePostRequest.Image;
            existingPost.Like = updatePostRequest.Like;
            existingPost.Author = _dbcontext.Users.Find(updatePostRequest.AuthorId);
            //existingPost.Author.Id = updatePostRequest.AuthorId;
            existingPost.Visible = updatePostRequest.Visible;
            existingPost.UpdatedDate = DateTime.Now;
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }

        public bool DeletePost(Guid postId)
        {
            var existingPost = _dbcontext.Posts.Find(postId);

            if (existingPost == null) return false;
            _dbcontext.Posts.Remove(existingPost);
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }
    }
}

