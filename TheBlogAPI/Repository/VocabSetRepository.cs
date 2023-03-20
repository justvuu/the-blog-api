using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Services;

namespace TheBlogAPI.Repository
{
    public class VocabSetRepository : IVocabSetRepository
    {
        private readonly TheBlogDbContext _dbcontext;

        public VocabSetRepository(TheBlogDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public bool CreateVocabSet(CreateVocabSetDTO createVocabSetDTO)
        {
            VocabSet vocabSet = new VocabSet();
            var existingSet = _dbcontext.VocabSet.FirstOrDefault(c => c.Nickname == createVocabSetDTO.Nickname);
            if (existingSet != null) return false;
            vocabSet.Nickname = createVocabSetDTO.Nickname.Trim();
            vocabSet.Times = 0;
            vocabSet.CreateTime = DateTime.Now;
            vocabSet.Id = Guid.NewGuid();
            _dbcontext.VocabSet.Add(vocabSet);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public bool DeleteVocabSet(Guid id)
        {
            VocabSet existingVocabSet = _dbcontext.VocabSet.Find(id);
            if (existingVocabSet == null) return false;
            _dbcontext.VocabSet.Remove(existingVocabSet);
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<VocabSet> GetAll(int pageIndex, int pageSize)
        {
            return _dbcontext.VocabSet.OrderByDescending(v => v.CreateTime).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public VocabSet GetVocabSetById(Guid id)
        {
            return _dbcontext.VocabSet.FirstOrDefault(v => v.Id == id);
        }

        public VocabSet GetVocabSetByNickname(string nickname)
        {
            return _dbcontext.VocabSet.FirstOrDefault(v => v.Nickname == nickname);
        }

        public bool UpdateVocabSet(VocabSet vocabSet, UpdateVocabSetDTO updateVocabSetDTO)
        {
            int times = updateVocabSetDTO.Times;
            vocabSet.Times = times;
            if (!string.IsNullOrEmpty(updateVocabSetDTO.Nickname))
            {
                vocabSet.Nickname = updateVocabSetDTO.Nickname.Trim();
            }

            if(times == 0)
            {
                //pass
            }

            else if(times == 1)
            {
                Random rnd = new Random();
                CreateEvent createEvent = new CreateEvent();
                vocabSet.CreateTime = DateTime.Now;
                createEvent.Start($"Review {vocabSet.Nickname}", vocabSet.CreateTime.AddMinutes(rnd.Next(20, 30)));
            }
            else if(times == 2)
            {
                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabSet.Nickname}", vocabSet.CreateTime.AddDays(1));
            }
            else if(times == 3)
            {
                Random rnd = new Random();
                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabSet.Nickname}", vocabSet.CreateTime.AddDays(rnd.Next(21, 29)));
            }
            else if(times == 4)
            {
                Random rnd = new Random();

                CreateEvent createEvent = new CreateEvent();
                createEvent.Start($"Review {vocabSet.Nickname}", vocabSet.CreateTime.AddDays(rnd.Next(60, 91)));
            }
            
            var check = _dbcontext.SaveChanges();
            return check != 0 ? true : false;
        }
    }
}

