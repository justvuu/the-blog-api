using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;


namespace TheBlogAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	
	public class CategoriesController:Controller
	{
		private CategoryService service;
		private TheBlogDbContext _dbContext;
		public CategoriesController(TheBlogDbContext dbContext)
		{
			_dbContext = dbContext;
			service = new CategoryService(dbContext);
        }

		[HttpGet]
		public IActionResult GetAllCategories()
		{
			var categories = service.GetAll();
			return Ok(categories);
		}

		//[HttpGet]
		//[Route("{id:guid}")]
		//[ActionName("GetCategoryById")]
		//public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
  //      {
		//	var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
		//	if(category != null)
		//	{
  //              return Ok(category);
  //          }
		//	return NotFound();
            
  //      }

		//[HttpPost]
		//public async Task<IActionResult> AddCategory(AddCategoryDTO addCategoryRequest)
		//{
		//	var category = new Category()
		//	{
		//		Name = addCategoryRequest.Name,
		//		UrlHandle = addCategoryRequest.UrlHandle
		//	};
		//	category.Id = Guid.NewGuid();
		//	await dbContext.Categories.AddAsync(category);
		//	await dbContext.SaveChangesAsync();
		//	return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
		//}

		//[HttpPut]
		//[Route("{id:guid}")]
  //      public async Task<IActionResult> AddCategory([FromRoute] Guid id, EditCategoryDTO updateCategoryRequest)
  //      {
		//	var existingCategory = await dbContext.Categories.FindAsync(id);
		//	if(existingCategory != null)
		//	{
		//		existingCategory.Name = updateCategoryRequest.Name;
		//		existingCategory.UrlHandle = updateCategoryRequest.UrlHandle;
  //              await dbContext.SaveChangesAsync();
		//		return Ok(existingCategory);
  //          }
		//	return NotFound();
  //      }

		//[HttpDelete]
  //      [Route("{id:guid}")]
  //      public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
  //      {
  //          var existingCategory = await dbContext.Categories.FindAsync(id);
  //          if (existingCategory != null)
  //          {
		//		dbContext.Categories.Remove(existingCategory);
  //              await dbContext.SaveChangesAsync();
  //              return Ok(existingCategory);
  //          }
  //          return BadRequest();
  //      }
    }
}

