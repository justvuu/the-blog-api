using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IArticleRepository
	{
		ICollection<Article> GetAll(int pageIndex, int pageSize);
        ICollection<ArticleSearchDTO> Search(string word);
		ICollection<ArticleSearchDTO> GetByCategory(string categorySlug);
        bool CreateArticle(CreateArticleDTO createArticleDTO);
		Article GetBySlug(string slug);
		Article GetById(Guid id);
		bool UpdateArticle(Guid articleId, UpdateArticleDTO updateArticleDTO);
		bool DeleteArticle(Guid articleId);
	}
}

