using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Repository
{
	public class UserRepository:IUserRepository
	{
		private readonly TheBlogDbContext _dbcontext;

		public UserRepository(TheBlogDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public ICollection<User> GetAll()
		{
			return _dbcontext.Users.OrderBy(u => u.Id).ToList();
		}

        public User GetUserById(Guid id)
		{
			return _dbcontext.Users.FirstOrDefault(u => u.Id == id);
		}

        public ICollection<User> GetUser(string name)
		{
			return _dbcontext.Users.Where(u => u.Username.Contains(name)).ToList();
		}

		public bool CreateUser(AddUserDTO createUserRequest)
		{
			var user = new User()
			{
				Username = createUserRequest.Username,
				FullName = createUserRequest.FullName,
				Avatar = createUserRequest.Avatar
			};
			user.Id = Guid.NewGuid();
			_dbcontext.Users.Add(user);
			var check =  _dbcontext.SaveChanges();
			if (check != 0) return true;
			return false;
		}

        public bool UpdateUser(User user, EditUserDTO updateUserRequest)
		{
			user.FullName = updateUserRequest.FullName;
			user.Avatar = updateUserRequest.Avatar;
			user.Username = updateUserRequest.Username;
			var check = _dbcontext.SaveChanges();
			if (check != 0) return true;
			return false;
        }
    }
}

