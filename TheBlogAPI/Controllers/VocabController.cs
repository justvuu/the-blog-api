using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VocabController : Controller
	{
        private readonly TheBlogDbContext dbContext;
        private readonly VocabService service;
		public VocabController(TheBlogDbContext dbContext)
		{
            this.dbContext = dbContext;
            service = new VocabService(dbContext);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocab>))]
        public IActionResult GetVocabylary()
        {
            var vocabs =  service.GetAll();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabs);
        }

        [HttpGet("get-by-cate/{setId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocab>))]
        public IActionResult GetVocabBySetId(Guid setId)
        {
            var vocabs = service.GetVocabBySetId(setId);
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabs);
        }

        [HttpGet("id/{vocabId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Vocab))]
        [ProducesResponseType(400)]
        public IActionResult GetById(Guid vocabId)
        {
            var vocab = service.GetVocabById(vocabId);
            if (vocab != null) return Ok(vocab);
            return NotFound("Do not exist !");
        }

        [HttpGet("{word}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vocab>))]
        [ProducesResponseType(400)]
        public IActionResult GetByWord(string word)
        {
            var vocab = service.GetVocabByWord(word);
            if (vocab != null) return Ok(vocab);
            return NotFound("Do not exist !");
        }

        [HttpGet("quiz")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuizDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetQuiz()
        {
            
            var quizzes = service.GetQuiz();
            if (quizzes != null) return Ok(quizzes);
            return NotFound("Do not exist !");
        }

        [HttpGet("get-remind-time")]
        public IActionResult GetEarliestRemindTime()
        {
            DateTime remindTime = service.GetEarliestRemindTime();
            if (remindTime != null) return Ok(remindTime);
            return NotFound("Do not exist !");
        }

        //public class RemineObject {
        //   public DateTime RemindTime { get; set; }
        //}

        //[HttpPost("get-vocab-by-remind-time")]
        //public IActionResult GetByRemindTime([FromBody] RemineObject remind)
        //{
        //    List<Vocab> vocabs = service.GetByRemindTime(remind.RemindTime).ToList();
        //    if (!ModelState.IsValid) return BadRequest();
        //    return Ok(vocabs);
        //}

        [HttpGet("get-vocab-by-remind-time")]
        public IActionResult GetByRemindTime()
        {
            List<Vocab> vocabs = service.GetByRemindTime().ToList();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(vocabs);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVocab(CreateVocabDTO createVocabDTO)
        {
            var isOK = service.CreateVocab(createVocabDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{vocabId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVocab(Guid vocabId, [FromBody] UpdateVocabDTO updateVocabDTO)
        {
            if (updateVocabDTO == null)
                return BadRequest(ModelState);
            var vocab = service.GetVocabById(vocabId);
            if (vocab == null)
                return NotFound();
            var check = service.UpdateVocab(vocab, updateVocabDTO);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating vocabulary");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfull");
        }

        [HttpPut("update-level/{vocabId}")]
        public IActionResult UpdateLevel(Guid vocabId)
        {
            bool check = service.UpdateLevel(vocabId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating level");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{vocabId}")]
        [Authorize]
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

