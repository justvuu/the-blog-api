using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IVocabRepository
	{
		ICollection<Vocab> GetAll();
		Vocab GetVocabById(Guid Id);
        ICollection<Vocab> GetVocabByWord(string word);
		ICollection<Vocab> GetVocabBySetId(Guid id);
		ICollection<QuizDTO> GetQuiz();
		bool CreateVocab(CreateVocabDTO createVocabRequest);
		bool UpdateVocab(Vocab vocab, UpdateVocabDTO updateVocabRequest);
		bool UpdateLevel(Guid id);
		bool DeleteVocab(Guid vocabId);
		DateTime GetEarliestRemindTime();
		ICollection<Vocab> GetByRemindTime();
    }
}

