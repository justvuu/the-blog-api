using System;
using Microsoft.AspNetCore.Mvc;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;
using TheBlogAPI.Services;

namespace TheBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabCategoryController : Controller
	{
        private readonly TheBlogDbContext dbContext;
        private readonly VocabCategoryService service;

		public VocabCategoryController(TheBlogDbContext dbContext)
		{
            this.dbContext = dbContext;
            service = new VocabCategoryService(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VocabCategory>))]
        public IActionResult GetVocabCategory()
        {
            var vocabCates = service.GetAll();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabCates);
        }

        [HttpGet("{cateId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VocabCategory>))]
        public IActionResult GetVocabCategoryById(Guid cateId)
        {
            var vocabCates = service.GetVocabCategoryById(cateId);
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabCates);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVocabCate(AddVocabCategoryDTO createVocabCategoryRequest)
        {
            var isOK = service.CreateVocabCategory(createVocabCategoryRequest);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{vocabCateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVocabCate(Guid vocabCateId, [FromBody] EditVocabCategoryDTO updateVocabCategoryRequest)
        {
            if (updateVocabCategoryRequest == null)
                return BadRequest(ModelState);
            var vocabCate = service.GetVocabCategoryById(vocabCateId);
            if (vocabCate == null)
                return NotFound();
            var check = service.UpdateVocabCategory(vocabCate, updateVocabCategoryRequest);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{vocabCateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVocabCate(Guid vocabCateId)
        {
            var check = service.DeleteVocabCategory(vocabCateId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

