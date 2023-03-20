using System;
namespace TheBlogAPI.Models.Entities
{
	public class Subscriber
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}

