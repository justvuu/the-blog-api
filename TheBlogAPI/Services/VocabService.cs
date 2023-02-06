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

        public ICollection<Vocab> GetAll() => repository.GetAll();

        public Vocab GetVocabById(Guid id)
        {
            return repository.GetVocabById(id);
        }

        public ICollection<Vocab> GetVocabByWord(string word)
        {
            return repository.GetVocabByWord(word);
        }

        public bool CreateVocab(CreateVocabDTO createVocabDTO)
        {
            return repository.CreateVocab(createVocabDTO);
        }

        public bool UpdateVocab(Vocab vocab, UpdateVocabDTO updateVocabDTO)
        {
            return repository.UpdateVocab(vocab, updateVocabDTO);
        }

        public bool DeleteVocab(Guid vocabId)
        {
            return repository.DeleteVocab(vocabId);
        }

        public ICollection<Vocab> GetVocabBySetId(Guid id)
        {
            return repository.GetVocabBySetId(id);
        }

        public ICollection<QuizDTO> GetQuiz()
        {
            return repository.GetQuiz();
        }
    }
}

