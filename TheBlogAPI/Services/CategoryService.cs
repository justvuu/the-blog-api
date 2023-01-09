using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
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
    }
}

