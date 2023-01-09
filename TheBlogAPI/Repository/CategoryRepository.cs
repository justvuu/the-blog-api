using System;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly TheBlogDbContext _dbContext;

		public CategoryRepository(TheBlogDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public bool AddCategory(AddCategoryDTO addCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditCategory(EditCategoryDTO editCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
    }
}

