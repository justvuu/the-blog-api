using System;
namespace TheBlogAPI.Models.Entities
{
	public class Vocab
	{
		public Guid Id { get; set; }

		public string Word { get; set; }

		public string VN { get; set; }

		public string EN { get; set; }

		public string? Pronunciation { get; set; }

		public string? Sound { get; set; }

		public string? Image { get; set; }

		public string Example { get; set; }

		public int Level { get; set; }

		public DateTime? CreateTime { get; set; }

		public DateTime? RemindTime { get; set; }

		public Guid SetId { get; set; }
	}
}

