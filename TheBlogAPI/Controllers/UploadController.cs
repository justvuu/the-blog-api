﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace TheBlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController:ControllerBase
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		public UploadController(IWebHostEnvironment webHostEnvironment)
		{
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public IActionResult UploadFiles(IFormCollection data)
        {
            List<IFormFile> files = (List<IFormFile>)data.Files;
            if (files.Count == 0) return BadRequest();
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images");
            foreach(var file in files)
            {
                string filePath = Path.Combine(directoryPath, file.FileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok("Successful!");


        }
	}
}
