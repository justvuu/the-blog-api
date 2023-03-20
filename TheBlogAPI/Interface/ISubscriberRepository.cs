using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface ISubscriberRepository
	{
        ICollection<Subscriber> GetAll();
        bool Create(CreateSubscriberDTO createSubscriberDTO);
        Subscriber GetById(Guid id);
    }
}

