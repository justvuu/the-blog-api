using System;
namespace TheBlogAPI.Models.Entities
{
	public class VocabCategory
	{
		public Guid Id { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime RecentlyTime { get; set; }

		public int Times { get; set; }

		public string Nickname { get; set; }

		public DateTime OpenTime { get; set; }
	}
}

