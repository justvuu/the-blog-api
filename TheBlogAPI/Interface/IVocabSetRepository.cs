using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IVocabSetRepository
	{
		ICollection<VocabSet> GetAll();
		VocabSet GetVocabSetById(Guid id);
        VocabSet GetVocabSetByNickname(string nickname);
		bool CreateVocabSet(CreateVocabSetDTO createVocabSetDTO);
		bool UpdateVocabSet(VocabSet vocabSet, UpdateVocabSetDTO updateVocabSetDTO);
		bool DeleteVocabSet(Guid id);
    }
}

