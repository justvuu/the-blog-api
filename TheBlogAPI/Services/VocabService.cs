using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class VocabService
	{
		private readonly TheBlogDbContext dbContext;
		private readonly IVocabRepository repository;

        public VocabService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			repository = new VocabRepository(dbContext);
        }

        public ICollection<Vocabulary> GetAll() => repository.GetAll();

        public Vocabulary GetVocabById(Guid id)
        {
            return repository.GetVocabById(id);
        }

        public ICollection<Vocabulary> GetVocabByWord(string word)
        {
            return repository.GetVocabByWord(word);
        }

        public bool CreateVocab(AddVocabDTO addVocabDTO)
        {
            return repository.CreateVocab(addVocabDTO);
        }

        public bool UpdateVocab(Vocabulary vocab, EditVocabDTO editVocabDTO)
        {
            return repository.UpdateVocab(vocab, editVocabDTO);
        }

        public bool DeleteVocab(Guid vocabId)
        {
            return repository.DeleteVocab(vocabId);
        }

        public ICollection<Vocabulary> GetVocabByCateId(Guid id)
        {
            return repository.GetVocabByCateId(id);
        }

        public ICollection<QuizDTO> GetQuiz()
        {
            return repository.GetQuiz();
        }
    }
}

