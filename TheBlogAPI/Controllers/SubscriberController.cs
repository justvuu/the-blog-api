using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBlogAPI.Data;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Services;

namespace TheBlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SubscriberController : Controller
	{
        private SubscriberService service;
        private TheBlogDbContext _dbContext;
        public SubscriberController(TheBlogDbContext dbContext)
        {
            _dbContext = dbContext;
            service = new SubscriberService(dbContext);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDTO createSubscriberDTO)
        {
            var isOK = service.Create(createSubscriberDTO);
            if (!isOK)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }
    }
}

