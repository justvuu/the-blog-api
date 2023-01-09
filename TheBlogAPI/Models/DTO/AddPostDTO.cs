using System;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Models.DTO
{
	public class AddPostDTO
	{
        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public string Path { get; set; }

        public string Image { get; set; }

        public int Like { get; set; }

        public bool Visible { get; set; }

        public Guid AuthorId { get; set; }
    }
}

