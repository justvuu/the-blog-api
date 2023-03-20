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

        public ICollection<Vocab> GetAll()
        {
            return _dbcontext.Vocab.OrderBy(v => v.Id).ToList();
        }

        public Vocab GetVocabById(Guid id)
        {
            return _dbcontext.Vocab.FirstOrDefault(v => v.Id == id);
        }

        public ICollection<Vocab> GetVocabByWord(string word)
        {
            return _dbcontext.Vocab.Where(v => v.Word.Contains(word)).ToList();
        }

        public bool CreateVocab(CreateVocabDTO createVocabDTO)
        {
            Vocab newWord = new Vocab();
            newWord.Word = createVocabDTO.Word.Trim();
            newWord.VN = createVocabDTO.VN.Trim();
            newWord.EN = createVocabDTO.EN.Trim();
            newWord.Example = createVocabDTO.Example.Trim();
            newWord.SetId = createVocabDTO.SetId;
            newWord.Level = createVocabDTO.Level;
            if (!string.IsNullOrEmpty(createVocabDTO.Image))
            {
                newWord.Image = createVocabDTO.Image.Trim();
            }
            if (!string.IsNullOrEmpty(createVocabDTO.Sound))
            {
                newWord.Sound = createVocabDTO.Sound.Trim();
            }
            if (!string.IsNullOrEmpty(createVocabDTO.Pronunciation))
            {
                newWord.Pronunciation = createVocabDTO.Pronunciation.Trim();
            }
            newWord.Id = Guid.NewGuid();
            newWord.CreateTime = DateTime.Now;
            newWord.Level = 0;
            _dbcontext.Vocab.Add(newWord);
            var check = _dbcontext.SaveChanges();
            if (check == 0) return false;
            return true;
        }

        public bool UpdateLevel(Guid id) {
            Vocab vocab = _dbcontext.Vocab.FirstOrDefault(c => c.Id == id);
            if (vocab == null) return false;
            int level = vocab.Level;
            if (level == 0)
            {
                vocab.CreateTime = DateTime.Now;
            }
            else if (level == 1)
            {
                vocab.RemindTime = vocab.CreateTime?.AddMinutes(20);
            }
            else if (level == 2)
            {
                vocab.RemindTime = vocab.CreateTime?.AddHours(9);
            }
            else if (level == 3)
            {
                vocab.RemindTime = vocab.CreateTime?.AddDays(1);
            }
            else if (level == 4)
            {
                vocab.RemindTime = vocab.CreateTime?.AddDays(7);
            }
            else if (level == 5)
            {
                vocab.RemindTime = vocab.CreateTime?.AddDays(21);
            }
            else if (level == 6)
            {
                vocab.RemindTime = vocab.CreateTime?.AddMonths(2);
            }
            else if (level == 7)
            {
                vocab.RemindTime = vocab.CreateTime?.AddMonths(3);
            }
            else if (level == 8)
            {
                vocab.RemindTime = vocab.CreateTime?.AddMonths(5);
            }
            vocab.Level = level + 1;

            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;

        }

        public bool UpdateVocab(Vocab vocab, UpdateVocabDTO updateVocabDTO)
        {
            if (!string.IsNullOrEmpty(updateVocabDTO.Word))
            {
                vocab.Word = updateVocabDTO.Word.Trim();
            }
            if(!string.IsNullOrEmpty(updateVocabDTO.EN))
            {
                vocab.EN = updateVocabDTO.EN.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.VN))
            {
                vocab.VN = updateVocabDTO.VN.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.Example))
            {   
                vocab.Example = updateVocabDTO.Example.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.Pronunciation))
            {
                vocab.Pronunciation = updateVocabDTO.Pronunciation.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.Image))
            {
                vocab.Image = updateVocabDTO.Image.Trim();
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.Sound))
            {
                vocab.Sound = updateVocabDTO.Sound.Trim();
            }
            if (updateVocabDTO.SetId != Guid.Empty && updateVocabDTO.SetId != null)
            {
                vocab.SetId = (Guid)updateVocabDTO.SetId;
            }
            if (!string.IsNullOrEmpty(updateVocabDTO.RemindTime))
            {
                int level = (int)updateVocabDTO.Level;
                if (level == 1) {
                    vocab.RemindTime = DateTime.Parse(updateVocabDTO.RemindTime).AddMinutes(10);
                }
                if (level == 2)
                {
                    vocab.RemindTime = DateTime.Parse(updateVocabDTO.RemindTime).AddDays(1);
                }
                else if (level == 3)
                {
                    vocab.RemindTime = DateTime.Parse(updateVocabDTO.RemindTime).AddDays(6);
                }
                else if (level == 4)
                {
                    vocab.RemindTime = DateTime.Parse(updateVocabDTO.RemindTime).AddDays(21);
                }
                else if (level == 5)
                {
                    vocab.RemindTime = DateTime.Parse(updateVocabDTO.RemindTime).AddMonths(1);
                }
                vocab.Level = level;

            }
            else if (updateVocabDTO.Level.HasValue)
            {
                int level = (int)updateVocabDTO.Level;
                if (level == 2)
                {
                    vocab.RemindTime = DateTime.Now.AddDays(1);
                }
                else if (level == 3)
                {
                    vocab.RemindTime = DateTime.Now.AddDays(6);
                }
                else if (level == 4)
                {
                    vocab.RemindTime = DateTime.Now.AddDays(21);
                }
                else if (level == 5) {
                    vocab.RemindTime = DateTime.Now.AddMonths(1);
                }
                vocab.Level = level;
            }

            
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool DeleteVocab(Guid vocabId)
        {
            var existingVocab = _dbcontext.Vocab.Find(vocabId);

            if (existingVocab == null) return false;
            _dbcontext.Vocab.Remove(existingVocab);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<Vocab> GetVocabBySetId(Guid id)
        {
            return _dbcontext.Vocab.Where(v => v.SetId == id).ToList();
        }

        public DateTime GetEarliestRemindTime() {
            //Vocab vocab = _dbcontext.Vocab.OrderByDescending(c => c.RemindTime).Take(1).ToList()[0];
            Vocab vocab = _dbcontext.Vocab.Where(c => c.RemindTime.HasValue).OrderBy(c => c.RemindTime).Take(1).ToList()[0];
            return (DateTime)vocab.RemindTime;
        }

        public ICollection<Vocab> GetByRemindTime() {
            DateTime remindTime = GetEarliestRemindTime();
            List<Vocab> All = _dbcontext.Vocab.Where(c => c.RemindTime.HasValue).ToList(); 
            List<Vocab> vocabs = All.Where(c => DateTime.Compare((DateTime)c.RemindTime, remindTime) == 0).ToList();
            return vocabs;
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
            List<Vocab> vocabularies = _dbcontext.Vocab.ToList();
            vocabularies = vocabularies.OrderBy(x => rnd.Next()).Take(80).ToList();
            List<Vocab> selectedWords = vocabularies.Take(20).ToList();
            List<Vocab> ansWords = vocabularies.GetRange(19, 60).ToList();

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

