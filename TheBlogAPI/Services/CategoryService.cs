using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class CategoryService
	{
        private readonly ICategoryRepository _repository;
        private TheBlogDbContext _dbContext;
        public CategoryService(TheBlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new CategoryRepository(dbContext);   
        }

        public List<Category> GetAll()
        {
            var categories = _repository.GetAll().ToList();
            return categories;
        }

        public Category GetCategoryById(Guid id)
        {
            return _repository.GetCategoryById(id);
        }
        public Category GetCategoryBySlug(string slug)
        {
            return _repository.GetCategoryBySlug(slug);
        }

        public bool CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            return _repository.CreateCategory(createCategoryDTO);
        }

        public bool UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDTO)
        {
            return _repository.UpdateCategory(id, updateCategoryDTO);
        }

        public bool DeleteCategory(Guid id)
        {
            return _repository.DeleteCategory(id);
        }
    }
}

