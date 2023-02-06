using System;
namespace TheBlogAPI.Models.Entities
{
	public class VocabSet
	{
		public Guid Id { get; set; }

		public DateTime CreateTime { get; set; }

		public int Times { get; set; }

		public string Nickname { get; set; }
	}
}

