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

        public bool CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var existedCate = _dbContext.Category.FirstOrDefault(c => c.Name == createCategoryDTO.Name);
            if (existedCate != null) { return false; }
            var category = new Category()
            {
                Name = createCategoryDTO.Name,
                Slug = createCategoryDTO.Slug
            };
            category.Id = Guid.NewGuid();
            _dbContext.Category.Add(category);
            var check = _dbContext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool DeleteCategory(Guid id)
        {
            Category cate = _dbContext.Category.Find(id);
            if (cate == null) { return false; }
            _dbContext.Category.Remove(cate);
            var check = _dbContext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool UpdateCategory(Guid cateId, UpdateCategoryDTO updateCategoryDTO)
        {
            Category cate = _dbContext.Category.Find(cateId);
            if(cate == null) { return false; }
            if (!string.IsNullOrEmpty(updateCategoryDTO.Name))
            {
                cate.Name = updateCategoryDTO.Name.Trim();
            }
            if(!string.IsNullOrEmpty(updateCategoryDTO.Slug))
            {
                cate.Slug  = updateCategoryDTO.Slug.Trim();
            }
            var check = _dbContext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<Category> GetAll()
        {
            return _dbContext.Category.ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            return _dbContext.Category.FirstOrDefault(c => c.Id == id);
        }

        public Category GetCategoryBySlug(string slug)
        {
            return _dbContext.Category.FirstOrDefault(c => c.Slug.Trim() == slug.Trim());
        }
    }
}

