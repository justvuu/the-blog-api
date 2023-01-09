using System;
namespace TheBlogAPI.Models.Entities
{
	public class Post
	{
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public string Path { get; set; }

        public string Image { get; set; }

        public int Like { get; set; }

        public bool Visible { get; set; }

        public User Author { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

