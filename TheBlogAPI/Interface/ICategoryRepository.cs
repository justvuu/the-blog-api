using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetAll();
		bool AddCategory(AddCategoryDTO addCategoryDTO);
		bool EditCategory(EditCategoryDTO editCategoryDTO);
		bool DeleteCategory(Guid id);
	}
}

