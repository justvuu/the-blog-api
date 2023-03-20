using System;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class SubscriberService
	{
        private readonly ISubscriberRepository _repository;
        private TheBlogDbContext _dbContext;
        public SubscriberService(TheBlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new SubscriberRepository(dbContext);
        }

        public List<Subscriber> GetAll()
        {
            var subscribers = _repository.GetAll().ToList();
            return subscribers;
        }

        public Subscriber GetSubscriber(Guid id)
        {
            return _repository.GetById(id);
        }
        public bool Create(CreateSubscriberDTO createSubscriberDTO)
        {
            return _repository.Create(createSubscriberDTO);
        }
    }
}

