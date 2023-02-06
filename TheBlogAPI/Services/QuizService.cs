
using System;
using TheBlogAPI.Data;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Services
{
	public class QuizService
	{
		
        private readonly TheBlogDbContext dbContext;
		private readonly VocabService vocabService;

        public QuizService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			vocabService = new VocabService(dbContext);

        }

		public List<QuizDTO> GetQuiz()
		{
			Random rnd = new Random();
			List<QuizDTO> quizzes = new List<QuizDTO>();
			List<Vocab> vocabularies = (List<Vocab>)vocabService.GetAll();
			vocabularies = vocabularies.OrderBy(x => rnd.Next()).Take(80).ToList();
			List<Vocab> selectedWords = vocabularies.Take(20).ToList();
			List<Vocab> ansWords = vocabularies.GetRange(19, 60).ToList();

			List<string> quizType = new List<string>()
			{
				"e-v",
				"v-e",
				//"fill_in"
			};

			foreach (var vocab in selectedWords)
			{
                //int type_index = rnd.Next(0, 2);
                int type_index = 1;
				if(type_index == 0)
				{
                    List<string> choices = new List<string>();
                    for (int i = 0; i < 3; i++)
                    {
                        choices.Add(ansWords[i].VN);
                    }
                    ansWords.RemoveRange(0, 3);
                    QuizDTO quizModel = new QuizDTO()
                    {
                        word = vocab.Word,
                        ans = vocab.VN,
                        image = vocab.Image,
                        pronunc = vocab.Pronunciation,
                        choices = choices
                    };
                    quizzes.Add(quizModel);
                }
				else if (type_index == 1)
				{
                    List<string> choices = new List<string>();
                    for (int i = 0; i < 3; i++)
                    {
                        choices.Add(ansWords[i].Word);
                    }
                    ansWords.RemoveRange(0, 3);
                    QuizDTO quizModel = new QuizDTO()
                    {
                        word = vocab.VN,
                        ans = vocab.Word,
                        image = vocab.Image,
                        pronunc = vocab.Pronunciation,
                        choices = choices
                    };
                    quizzes.Add(quizModel);
                }
				
                
			}
			return quizzes;
		}
	}
}

