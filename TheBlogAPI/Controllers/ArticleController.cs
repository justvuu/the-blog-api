using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class ArticleController : Controller
    {
        private readonly ArticleService service;
        private readonly TheBlogDbContext dbContext;
        private readonly CreateEvent _event;
        public ArticleController(TheBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
            service = new ArticleService(dbContext);
        }

        [HttpPost("get-articles")]
        [AllowAnonymous]
        public IActionResult GetAllVisible([FromBody] PageParameters parameters)
        {
            var articles = service.GetAllVisible(parameters.PageIndex, parameters.PageSize);
            return Ok(articles);
        }

        [HttpPost("get-all")]
        [AllowAnonymous]
        public IActionResult GetAll([FromBody] PageParameters parameters)
        {
            var articles = service.GetAll(parameters.PageIndex, parameters.PageSize);
            return Ok(articles);
        }

        [HttpGet("{slug}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(Article))]
        [ProducesResponseType(400)]
        public IActionResult GetBySlug(string slug)
        {
            var article = service.GetBySlug(slug);
            if (article != null) return Ok(article);
            return NotFound("Do not exist !");
        }

        [HttpGet("id/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(Article))]
        [ProducesResponseType(400)]
        public IActionResult GetById(Guid id)
        {
            var article = service.GetById(id);
            if (article != null) return Ok(article);
            return NotFound("Do not exist !");
        }

        [HttpGet("search/{word}")]
        [AllowAnonymous]
        public IActionResult Search(string word)
        {
            var articles = service.Search(word);
            if (articles != null) return Ok(articles); 
            return NotFound("Does not have any articles !");
        }

        [HttpGet("get-by-category/{categorySlug}")]
        [AllowAnonymous]
        public IActionResult GetByCategory(string categorySlug)
        {
            var articles = service.GetByCategory(categorySlug);
            if (articles != null) return Ok(articles);
            return NotFound("Does not have any articles !");
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArticle(CreateArticleDTO createArticleDTO)
        {
            var isOK = service.CreateArticle(createArticleDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{articleId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateArticle(Guid articleId, [FromBody] UpdateArticleDTO updateArticleDTO)
        {
            if (updateArticleDTO == null)
                return BadRequest(ModelState);
            
            var check = service.UpdateArticle(articleId, updateArticleDTO);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();


        }

        [HttpDelete("{articleId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArticle(Guid articleId)
        {
            var check = service.DeleteArticle(articleId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

