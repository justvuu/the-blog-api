using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;


namespace TheBlogAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
	[Route("api/[controller]")]
	[Authorize]
	
	public class CategoryController:Controller
	{
		private CategoryService service;
		private TheBlogDbContext _dbContext;
		public CategoryController(TheBlogDbContext dbContext)
		{
			_dbContext = dbContext;
			service = new CategoryService(dbContext);
        }

		[HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
		{
			var categories = service.GetAll();
			return Ok(categories);
		}

		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetCategoryById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
		{
			Category category = service.GetCategoryById(id);
			if (category != null)
			{
				return Ok(category);
			}
			return NotFound();

		}

        [HttpGet("get-by-slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            Category category = service.GetCategoryBySlug(slug);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();

        }

        [HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
		{
            var isOK = service.CreateCategory(createCategoryDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryDTO updateCategoryDTO)
		{
            var isOK = service.UpdateCategory(id, updateCategoryDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
		{
            var isOK = service.DeleteCategory(id);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }
	}
}

