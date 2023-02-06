using System;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Models.DTO
{
	public class UpdateArticleDTO
	{
        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public string Slug { get; set; }

        public string Image { get; set; }

        public bool Visible { get; set; }

        public string CategoryId { get; set; }
    }
}

