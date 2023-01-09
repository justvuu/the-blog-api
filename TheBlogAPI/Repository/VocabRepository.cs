using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;

namespace TheBlogAPI.Repository
{
	public class VocabRepository:IVocabRepository
	{
        private readonly TheBlogDbContext _dbcontext;

        public VocabRepository(TheBlogDbContext dbcontext)
		{
            _dbcontext = dbcontext;
		}

        public ICollection<Vocabulary> GetAll()
        {
            return _dbcontext.Vocabulary.OrderBy(v => v.Id).ToList();
        }

        public Vocabulary GetVocabById(Guid id)
        {
            return _dbcontext.Vocabulary.FirstOrDefault(v => v.Id == id);
        }

        public ICollection<Vocabulary> GetVocabByWord(string word)
        {
            return _dbcontext.Vocabulary.Where(v => v.Word.Contains(word)).ToList();
        }

        public bool CreateVocab(AddVocabDTO createVocabRequest)
        {
            Vocabulary newWord = new Vocabulary();
            newWord.Word = createVocabRequest.Word.Trim();
            newWord.VN = createVocabRequest.VN.Trim();
            newWord.EN = createVocabRequest.EN.Trim();
            newWord.Example = createVocabRequest.Example.Trim();
            newWord.CategoryId = createVocabRequest.CategoryId;
            if (!string.IsNullOrEmpty(createVocabRequest.Image))
            {
                newWord.Image = createVocabRequest.Image.Trim();
            }
            if (!string.IsNullOrEmpty(createVocabRequest.Sound))
            {
                newWord.Sound = createVocabRequest.Sound.Trim();
            }
            if (!string.IsNullOrEmpty(createVocabRequest.Pronunciation))
            {
                newWord.Pronunciation = createVocabRequest.Pronunciation.Trim();
            }
            newWord.Id = Guid.NewGuid();
            _dbcontext.Vocabulary.Add(newWord);
            var check = _dbcontext.SaveChanges();
            if (check == 0) return false;
            return true;
        }

        public bool UpdateVocab(Vocabulary vocab, EditVocabDTO updateVocabRequest)
        {
            if (!string.IsNullOrEmpty(updateVocabRequest.Word))
            {
                vocab.Word = updateVocabRequest.Word.Trim();
            }
            if(!string.IsNullOrEmpty(updateVocabRequest.EN))
            {
                vocab.EN = updateVocabRequest.EN.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabRequest.VN))
            {
                vocab.VN = updateVocabRequest.VN.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabRequest.Example))
            {
                vocab.Example = updateVocabRequest.Example.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabRequest.Pronunciation))
            {
                vocab.Pronunciation = updateVocabRequest.Pronunciation.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabRequest.Image))
            {
                vocab.Image = updateVocabRequest.Image.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabRequest.Sound))
            {
                vocab.Sound = updateVocabRequest.Sound.Trim();
            }
            if (updateVocabRequest.CategoryId != Guid.Empty && updateVocabRequest.CategoryId != null)
            {
                vocab.CategoryId = (Guid)updateVocabRequest.CategoryId;
            }
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool DeleteVocab(Guid vocabId)
        {
            var existingVocab = _dbcontext.Vocabulary.Find(vocabId);

            if (existingVocab == null) return false;
            _dbcontext.Vocabulary.Remove(existingVocab);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<Vocabulary> GetVocabByCateId(Guid id)
        {
            return _dbcontext.Vocabulary.Where(v => v.CategoryId == id).ToList();
        }

        public List<string> Shuffle(List<string> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public ICollection<QuizDTO> GetQuiz()
        {
            Random rnd = new Random();
            List<QuizDTO> quizzes = new List<QuizDTO>();
            List<Vocabulary> vocabularies = _dbcontext.Vocabulary.ToList();
            vocabularies = vocabularies.OrderBy(x => rnd.Next()).Take(80).ToList();
            List<Vocabulary> selectedWords = vocabularies.Take(20).ToList();
            List<Vocabulary> ansWords = vocabularies.GetRange(19, 60).ToList();

            List<string> quizType = new List<string>()
            {
                "EV",
                "VE",
                "fill_in"
            };

            foreach (var vocab in selectedWords)
            {
                int type_index = rnd.Next(0, 3);
                if(type_index == 0)
                {
                    List<string> choices = new List<string>();
                    for (int i = 0; i < 3; i++)
                    {
                        choices.Add(ansWords[i].VN);
                    }
                    ansWords.RemoveRange(0, 3);
                    choices.Add(vocab.VN.Trim());
                    QuizDTO quizModel = new QuizDTO()
                    {
                        word = vocab.Word.Trim(),
                        ans = vocab.VN.Trim(),
                        image = vocab.Image,
                        pronunc = vocab.Pronunciation,
                        quizType = quizType[type_index],
                        choices = Shuffle(choices)
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
                    choices.Add(vocab.Word.Trim());
                    QuizDTO quizModel = new QuizDTO()
                    {
                        word = vocab.VN.Trim(),
                        ans = vocab.Word.Trim(),
                        image = vocab.Image,
                        pronunc = vocab.Pronunciation,
                        quizType = quizType[type_index],
                        choices = Shuffle(choices)
                    };
                    quizzes.Add(quizModel);
                }
                else
                {
                    List<string> choices = new List<string>();
                    for (int i = 0; i < 3; i++)
                    {
                        choices.Add(ansWords[i].Word);
                    }
                    ansWords.RemoveRange(0, 3);
                    choices.Add(vocab.Word.Trim());
                    QuizDTO quizModel = new QuizDTO()
                    {
                        word = vocab.Word.Trim(),
                        ans = vocab.VN.Trim(),
                        image = vocab.Image,
                        en = vocab.EN,
                        pronunc = vocab.Pronunciation,
                        quizType = quizType[type_index],
                    };
                    quizzes.Add(quizModel);
                }
                
            }
            return quizzes;
        }
    }
}

