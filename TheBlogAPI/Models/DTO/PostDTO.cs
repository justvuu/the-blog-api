using System;
namespace TheBlogAPI.Models.DTO
{
	public class PostDTO
	{
        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public string Path { get; set; }

        public string Image { get; set; }

        public int Like { get; set; }

        public bool Visible { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

