using System;
namespace TheBlogAPI.Models.DTO
{
	public class QuizDTO
	{
		public string word { get; set; }

		public string ans { get; set; }

		public string pronunc { get; set; }

		public string image { get; set; }

		public string quizType { get; set; }

		public string en { get; set; }

		public List<string> choices { get; set; }

	}
}

