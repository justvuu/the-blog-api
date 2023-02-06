using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class VocabSetService
	{
		private readonly TheBlogDbContext dbContext;
		private readonly IVocabSetRepository repository;

		public VocabSetService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			repository = new VocabSetRepository(dbContext);
        }

        public bool CreateVocabSet(CreateVocabSetDTO createVocabSetDTO)
        {
            return repository.CreateVocabSet(createVocabSetDTO);
        }

        public bool DeleteVocabSet(Guid id)
        {
            return repository.DeleteVocabSet(id);
        }

        public ICollection<VocabSet> GetAll()
        {
            return repository.GetAll();
        }

        public VocabSet GetVocabSetById(Guid id)
        {
            return repository.GetVocabSetById(id);
        }

        public VocabSet GetVocabSetByNickname(string nickname)
        {
            return repository.GetVocabSetByNickname(nickname);
        }

        public bool UpdateVocabSet(VocabSet vocabCategory, UpdateVocabSetDTO updateVocabSetDTO)
        {
            return repository.UpdateVocabSet(vocabCategory, updateVocabSetDTO);
        }
    }
}

