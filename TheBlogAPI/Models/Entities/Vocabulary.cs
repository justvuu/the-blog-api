using System;
namespace TheBlogAPI.Models.Entities
{
	public class Vocabulary
	{
		public Guid Id { get; set; }

		public string Word { get; set; }

		public string VN { get; set; }

		public string EN { get; set; }

		public string? Pronunciation { get; set; }

		public string? Sound { get; set; }

		public string? Image { get; set; }

		public string Example { get; set; }

		public Guid CategoryId { get; set; }
	}
}

