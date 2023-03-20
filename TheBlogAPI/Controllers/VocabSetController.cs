using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;
using TheBlogAPI.Services;

namespace TheBlogAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VocabSetController : Controller
	{
        private readonly TheBlogDbContext dbContext;
        private readonly VocabSetService service;

		public VocabSetController(TheBlogDbContext dbContext)
		{
            this.dbContext = dbContext;
            service = new VocabSetService(dbContext);
        }

        [HttpPost("get-vocab-sets")]
        //[Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VocabSet>))]
        public IActionResult GetVocabSet([FromBody] PageParameters parameters)
        {
            var vocabSets = service.GetAll(parameters.PageIndex, parameters.PageSize);
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabSets);
        }

        [HttpGet("{setId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VocabSet>))]
        public IActionResult GetVocabSetById(Guid setId)
        {
            var vocabCates = service.GetVocabSetById(setId);
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabCates);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVocabSet(CreateVocabSetDTO createVocabSetDTO)
        {
            var isOK = service.CreateVocabSet(createVocabSetDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{setId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVocabSet(Guid setId, [FromBody] UpdateVocabSetDTO updateVocabSetDTO)
        {
            if (updateVocabSetDTO == null)
                return BadRequest(ModelState);
            var vocabSet = service.GetVocabSetById(setId);
            if (vocabSet == null)
                return NotFound();
            var check = service.UpdateVocabSet(vocabSet, updateVocabSetDTO);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{setId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVocabSet(Guid setId)
        {
            var check = service.DeleteVocabSet(setId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

