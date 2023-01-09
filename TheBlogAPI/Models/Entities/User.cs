using System;
namespace TheBlogAPI.Models.Entities
{
	public class User
	{
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }
    }
}

