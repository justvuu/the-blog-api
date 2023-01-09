using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;
using TheBlogAPI.Services;

namespace TheBlogAPI.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly PostService service;
        private readonly TheBlogDbContext dbContext;
        private readonly CreateEvent _event;
        public PostsController(TheBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
            service = new PostService(dbContext);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
           
            var posts = service.GetAll();
            return Ok(posts);
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        [ProducesResponseType(400)]
        public IActionResult GetBySlug(string slug)
        {
            var post = service.GetBySlug(slug);
            if (post != null) return Ok(post);
            return NotFound("Do not exist !");
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost(AddPostDTO addPostDTO)
        {
            var isOK = service.CreatePost(addPostDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(Guid postId, [FromBody] EditPostDTO updatePostRequest)
        {
            if (updatePostRequest == null)
                return BadRequest(ModelState);
            
            var check = service.UpdatePost(postId, updatePostRequest);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();


        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(Guid postId)
        {
            var check = service.DeletePost(postId);
            if (!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

