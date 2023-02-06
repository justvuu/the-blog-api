using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetAll();
		bool CreateCategory(CreateCategoryDTO createCategoryDTO);
		bool UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDTO);
		Category GetCategoryById(Guid id);
        Category GetCategoryBySlug(string slug);
        bool DeleteCategory(Guid id);
	}
}

