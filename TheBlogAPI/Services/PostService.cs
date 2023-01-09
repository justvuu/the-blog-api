using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class PostService
	{
		private readonly TheBlogDbContext dbContext;
		private readonly IPostRepository repository;
		public PostService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			repository = new PostRepository(dbContext);
        }

        public ICollection<Post> GetAll()
        {
			return repository.GetAll();
        }

        public Post GetBySlug(string slug)
        {
            return repository.GetBySlug(slug);
        }

        public bool CreatePost(AddPostDTO addPostDTO)
        {
            return repository.CreatePost(addPostDTO);
        }

        public bool UpdatePost(Guid postId, EditPostDTO editPostDTO)
        {
            return repository.UpdatePost(postId, editPostDTO);
        }

        public bool DeletePost(Guid postId)
        {
            return repository.DeletePost(postId);
        }
    }
}

