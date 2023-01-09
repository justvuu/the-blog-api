using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IVocabCategoryRepository
	{
		ICollection<VocabCategory> GetAll();
		VocabCategory GetVocabCategoryById(Guid id);
        VocabCategory GetVocabCategoryByNickname(string nickname);
		bool CreateVocabCategory(AddVocabCategoryDTO createVocabCategoryRequest);
		bool UpdateVocabCategory(VocabCategory vocabCategory, EditVocabCategoryDTO updateVocabCategoryRequest);
		bool DeleteVocabCategory(Guid id);
    }
}

