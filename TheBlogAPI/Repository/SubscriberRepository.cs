using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Repository
{
	public class SubscriberRepository : ISubscriberRepository
	{
        private readonly TheBlogDbContext _dbContext;

        public SubscriberRepository(TheBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(CreateSubscriberDTO createSubscriberDTO)
        {
            var existedEmail = _dbContext.Subscriber.FirstOrDefault(c => c.Email == createSubscriberDTO.Email);
            if (existedEmail != null) { return false; }
            var subscriber = new Subscriber()
            {
                Email = createSubscriberDTO.Email,
            };
            subscriber.Id = Guid.NewGuid();
            subscriber.CreatedDate = DateTime.Now;
            _dbContext.Subscriber.Add(subscriber);
            var check = _dbContext.SaveChanges();
            return check != 0 ? true : false;
        }

        public ICollection<Subscriber> GetAll()
        {
            return _dbContext.Subscriber.ToList();
        }

        public Subscriber GetById(Guid id)
        {
            return _dbContext.Subscriber.FirstOrDefault(s => s.Id == id);
        }
    }
}

