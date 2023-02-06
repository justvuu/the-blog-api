using System;
namespace TheBlogAPI.Models.Entities
{
	public class Article
	{
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public string Slug { get; set; }

        public string Image { get; set; }

        public bool Visible { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        
        public Category Category { get; set; }
    }
}

