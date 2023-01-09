using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;

namespace TheBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController:Controller
	{
        private readonly TheBlogDbContext dbContext;
        private readonly UserService service;
        public UsersController(TheBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
            service = new UserService(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = service.GetAll();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(users);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetById(Guid id)
        {
            var user = service.GetUserById(id);
            if (user != null) return Ok(user);
            return NotFound("Do not exist !");
        }

        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetByUserName(string username)
        {
            var user = service.GetUser(username);
            if(user != null) return Ok(user);
            return NotFound("Do not exist !");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser(AddUserDTO createUserRequest)
        {
            var isOK = service.CreateUser(createUserRequest);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(Guid userId, [FromBody] EditUserDTO updateUserRequest)
        {
            if (updateUserRequest == null)
                return BadRequest(ModelState);
            var user = service.GetUserById(userId);
            if (user == null)
                return NotFound();
            var check = service.UpdateUser(user, updateUserRequest);
            if(!check)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();

            
        }

    }
    
}

