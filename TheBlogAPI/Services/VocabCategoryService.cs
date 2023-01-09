using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class VocabCategoryService
	{
		private readonly TheBlogDbContext dbContext;
		private readonly IVocabCategoryRepository repository;

		public VocabCategoryService(TheBlogDbContext dbContext)
		{
			this.dbContext = dbContext;
			repository = new VocabCategoryRepository(dbContext);
        }

        public bool CreateVocabCategory(AddVocabCategoryDTO addVocabCategoryDTO)
        {
            return repository.CreateVocabCategory(addVocabCategoryDTO);
        }

        public bool DeleteVocabCategory(Guid id)
        {
            return repository.DeleteVocabCategory(id);
        }

        public ICollection<VocabCategory> GetAll()
        {
            return repository.GetAll();
        }

        public VocabCategory GetVocabCategoryById(Guid id)
        {
            return repository.GetVocabCategoryById(id);
        }

        public VocabCategory GetVocabCategoryByNickname(string nickname)
        {
            return repository.GetVocabCategoryByNickname(nickname);
        }

        public bool UpdateVocabCategory(VocabCategory vocabCategory, EditVocabCategoryDTO editVocabCategoryDTO)
        {
            return repository.UpdateVocabCategory(vocabCategory, editVocabCategoryDTO);
        }
    }
}

