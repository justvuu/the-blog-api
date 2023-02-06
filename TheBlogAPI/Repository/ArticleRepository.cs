using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Repository
{
	public class ArticleRepository:IArticleRepository
	{
        private readonly TheBlogDbContext _dbcontext;

        public ArticleRepository(TheBlogDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ICollection<Article> GetAll(int pageIndex, int pageSize)
        {
            return _dbcontext.Article.Include(c => c.Category).OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public Article GetBySlug(string slug)
        {
            return _dbcontext.Article.Include(c => c.Category).FirstOrDefault(p => p.Slug == slug);
        }

        public Article GetById(Guid id)
        {
            return _dbcontext.Article.Include(c => c.Category).FirstOrDefault(p => p.Id == id);
        }

        public ICollection<ArticleSearchDTO> Search(string word)
        {
            var articleSearchs = new List<ArticleSearchDTO>();
            var articles = _dbcontext.Article.Where(a => a.Title.Contains(word)).OrderByDescending(p => p.PublishDate).ToList();
            foreach (var article in articles)
            {
                var articleSearch = new ArticleSearchDTO
                {
                    Id = article.Id,
                    Title = article.Title,
                    Summary = article.Summary,
                    Slug = article.Slug,
                    Image = article.Image
                };
                articleSearchs.Add(articleSearch);
            }
            return articleSearchs;
        }

        public ICollection<ArticleSearchDTO> GetByCategory(string categorySlug)
        {
            var result = new List<ArticleSearchDTO>();
            var articles = _dbcontext.Article.Where(a => a.Category.Slug == categorySlug).OrderByDescending(p => p.PublishDate).ToList();
            foreach (var article in articles)
            {
                var articleSearch = new ArticleSearchDTO
                {
                    Id = article.Id,
                    Title = article.Title,
                    Summary = article.Summary,
                    Slug = article.Slug,
                    Image = article.Image
                };
                result.Add(articleSearch);
            }
            return result;
        }

        public bool CreateArticle(CreateArticleDTO createArticleDTO)
        {
            var article = new Article()
            {
                Title = createArticleDTO.Title,
                Content = createArticleDTO.Content,
                Summary = createArticleDTO.Summary,
                Slug = createArticleDTO.Slug,
                Image = createArticleDTO.Image,
                Visible = createArticleDTO.Visible,
                UpdatedDate = DateTime.Now,
                PublishDate = DateTime.Now
            };

            article.Id = Guid.NewGuid();
            article.Category = _dbcontext.Category.Find(createArticleDTO.CategoryId);
            _dbcontext.Article.Add(article);
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }

        public bool UpdateArticle(Guid articleId, UpdateArticleDTO updateArticleDTO)
        {
            var existingArticle = _dbcontext.Article.Find(articleId);

            if (existingArticle == null) return false;

            if (!String.IsNullOrEmpty(updateArticleDTO.Title))
            {
                existingArticle.Title = updateArticleDTO.Title;
            }

            if (!String.IsNullOrEmpty(updateArticleDTO.Content)) {
                existingArticle.Content = updateArticleDTO.Content;
            }

            if (!string.IsNullOrEmpty(updateArticleDTO.Summary)) {
                existingArticle.Summary = updateArticleDTO.Summary;
            }

            if (!String.IsNullOrEmpty(updateArticleDTO.Slug)) {
                existingArticle.Slug = updateArticleDTO.Slug;
            }

            if (!string.IsNullOrEmpty(updateArticleDTO.Image))
            {
                existingArticle.Image = updateArticleDTO.Image;
            }

            if (!String.IsNullOrEmpty(updateArticleDTO.CategoryId)) {
                existingArticle.Category = _dbcontext.Category.Find(new Guid(updateArticleDTO.CategoryId));
            }

            existingArticle.Visible = updateArticleDTO.Visible;
            existingArticle.UpdatedDate = DateTime.Now;
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }

        public bool DeleteArticle(Guid articleId)
        {
            var existingArticle = _dbcontext.Article.Find(articleId);

            if (existingArticle == null) return false;
            _dbcontext.Article.Remove(existingArticle);
            var check = _dbcontext.SaveChanges();
            if (check != 0) return true;
            return false;
        }

       
    }
}

