using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;
using TheBlogAPI.Repository;

namespace TheBlogAPI.Services
{
	public class UserService
	{
        private readonly TheBlogDbContext dbContext;
        private readonly IUserRepository repository;

        public UserService(TheBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
            repository = new UserRepository(dbContext);
        }

        public ICollection<User> GetAll()
        {
            return repository.GetAll();
        }

        public User GetUserById(Guid id)
        {
            return repository.GetUserById(id);
        }

        public ICollection<User> GetUser(string name)
        {
            return repository.GetUser(name);
        }

        public bool CreateUser(AddUserDTO addUserDTO)
        {
            return repository.CreateUser(addUserDTO);
        }

        public bool UpdateUser(User user, EditUserDTO editUserDTO)
        {
            return repository.UpdateUser(user, editUserDTO);
        }

    }
}

