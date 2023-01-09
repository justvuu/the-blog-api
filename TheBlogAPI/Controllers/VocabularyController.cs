using System;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
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
    public class VocabularyController : Controller
	{
        private readonly TheBlogDbContext dbContext;
        private readonly VocabService service;
		public VocabularyController(TheBlogDbContext dbContext)
		{
            this.dbContext = dbContext;
            service = new VocabService(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocabulary>))]
        public IActionResult GetVocabylary()
        {
            var vocabs =  service.GetAll();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabs);
        }

        [HttpGet("get-by-cate/{cateId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocabulary>))]
        public IActionResult GetVocabByCateID(Guid cateId)
        {
            var vocabs = service.GetVocabByCateId(cateId);
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabs);
        }

        [HttpGet("id/{vocabId}")]
        [ProducesResponseType(200, Type = typeof(Vocabulary))]
        [ProducesResponseType(400)]
        public IActionResult GetById(Guid vocabId)
        {
            var vocab = service.GetVocabById(vocabId);
            if (vocab != null) return Ok(vocab);
            return NotFound("Do not exist !");
        }

        [HttpGet("{word}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocabulary>))]
        [ProducesResponseType(400)]
        public IActionResult GetByWord(string word)
        {
            var vocab = service.GetVocabByWord(word);
            if (vocab != null) return Ok(vocab);
            return NotFound("Do not exist !");
        }

        [HttpGet("quiz")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuizDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetQuiz()
        {
            
            var quizzes = service.GetQuiz();
            if (quizzes != null) return Ok(quizzes);
            return NotFound("Do not exist !");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVocab(AddVocabDTO createVocabRequest)
        {
            var isOK = service.CreateVocab(createVocabRequest);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{vocabId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVocab(Guid vocabId, [FromBody] EditVocabDTO updateVocabRequest)
        {
            if (updateVocabRequest == null)
                return BadRequest(ModelState);
            var vocab = service.GetVocabById(vocabId);
            if (vocab == null)
                return NotFound();
            var check = service.UpdateVocab(vocab, updateVocabRequest);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{vocabId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVocab(Guid vocabId)
        {
            var check = service.DeleteVocab(vocabId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

