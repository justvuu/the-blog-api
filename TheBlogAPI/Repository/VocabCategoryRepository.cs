using System;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;

namespace TheBlogAPI.Repository
{
    public class VocabCategoryRepository : IVocabCategoryRepository
    {
        private readonly TheBlogDbContext _dbcontext;

        public VocabCategoryRepository(TheBlogDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public bool CreateVocabCategory(AddVocabCategoryDTO createVocabCategoryRequest)
        {
            VocabCategory vocabCategory = new VocabCategory();
            var existingCate = _dbcontext.VocabCategory.FirstOrDefault(c => c.Nickname == createVocabCategoryRequest.Nickname);
            if (existingCate != null) return false;
            vocabCategory.Nickname = createVocabCategoryRequest.Nickname.Trim();
            vocabCategory.Times = 0;
            vocabCategory.CreateTime = DateTime.Now;
            vocabCategory.RecentlyTime = DateTime.Now;
            vocabCategory.OpenTime = DateTime.Now;
            vocabCategory.Id = Guid.NewGuid();
            _dbcontext.VocabCategory.Add(vocabCategory);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool DeleteVocabCategory(Guid id)
        {
            VocabCategory existingVocabCategory = _dbcontext.VocabCategory.Find(id);
            if (existingVocabCategory == null) return false;
            _dbcontext.VocabCategory.Remove(existingVocabCategory);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<VocabCategory> GetAll()
        {
            return _dbcontext.VocabCategory.OrderByDescending(v => v.CreateTime).ToList();
        }

        public VocabCategory GetVocabCategoryById(Guid id)
        {
            return _dbcontext.VocabCategory.FirstOrDefault(v => v.Id == id);
        }

        public VocabCategory GetVocabCategoryByNickname(string nickname)
        {
            return _dbcontext.VocabCategory.FirstOrDefault(v => v.Nickname == nickname);
        }

        public bool UpdateVocabCategory(VocabCategory vocabCategory, EditVocabCategoryDTO updateVocabCategoryRequest)
        {
            int times = updateVocabCategoryRequest.Times;
            vocabCategory.Times = times;
            if (!string.IsNullOrEmpty(updateVocabCategoryRequest.Nickname))
            {
                vocabCategory.Nickname = updateVocabCategoryRequest.Nickname.Trim();
            }

            if(times == 0)
            {
                //pass
            }

            else if(times == 1)
            {
                Random rnd = new Random();
                CreateEvent createEvent = new CreateEvent();
                vocabCategory.CreateTime = DateTime.Now;
                createEvent.Start($"Review {vocabCategory.Nickname}", vocabCategory.CreateTime.AddMinutes(rnd.Next(20, 30)));
            }
            else if(times == 2)
            {
                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabCategory.Nickname}", vocabCategory.CreateTime.AddDays(1));
            }
            else if(times == 3)
            {
                Random rnd = new Random();
                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabCategory.Nickname}", vocabCategory.CreateTime.AddDays(rnd.Next(21, 29)));
            }
            else if(times == 4)
            {
                Random rnd = new Random();

                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabCategory.Nickname}", vocabCategory.CreateTime.AddDays(rnd.Next(60, 91)));
            }
            
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }
    }
}

