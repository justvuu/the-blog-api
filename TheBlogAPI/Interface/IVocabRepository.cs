using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IVocabRepository
	{
		ICollection<Vocabulary> GetAll();
		Vocabulary GetVocabById(Guid Id);
        ICollection<Vocabulary> GetVocabByWord(string word);
		ICollection<Vocabulary> GetVocabByCateId(Guid id);
		ICollection<QuizDTO> GetQuiz();
		bool CreateVocab(AddVocabDTO createVocabRequest);
		bool UpdateVocab(Vocabulary vocab, EditVocabDTO updateVocabRequest);
		bool DeleteVocab(Guid vocabId);
    }
}

