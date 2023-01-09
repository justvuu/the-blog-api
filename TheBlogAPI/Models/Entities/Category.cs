using System;
namespace TheBlogAPI.Models.Entities
{
	public class Category
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string UrlHandle { get; set; }
	}
}

