using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class ArticleService
	{
		private readonly TheBlogDbContext dbContext;
		private readonly IArticleRepository repository;
		public ArticleService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			repository = new ArticleRepository(dbContext);
        }

        public ICollection<Article> GetAll(int pageIndex, int pageSize)
        {
			return repository.GetAll(pageIndex, pageSize);
        }

        public ICollection<ArticleSearchDTO> Search(string word) {
            return repository.Search(word);
        }

        public ICollection<ArticleSearchDTO> GetByCategory(string categorySlug)
        {
            return repository.GetByCategory(categorySlug);
        }

        public Article GetBySlug(string slug)
        {
            return repository.GetBySlug(slug);
        }

        public Article GetById(Guid id)
        {
            return repository.GetById(id);
        }

        public bool CreateArticle(CreateArticleDTO addPostDTO)
        {
            return repository.CreateArticle(addPostDTO);
        }

        public bool UpdateArticle(Guid postId, UpdateArticleDTO editPostDTO)
        {
            return repository.UpdateArticle(postId, editPostDTO);
        }

        public bool DeleteArticle(Guid postId)
        {
            return repository.DeleteArticle(postId);
        }
    }
}

