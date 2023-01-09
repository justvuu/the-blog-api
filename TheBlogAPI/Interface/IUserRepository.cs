using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IUserRepository
	{
		ICollection<User> GetAll();
		User GetUserById(Guid id);
        ICollection<User> GetUser(string name);
		bool CreateUser(AddUserDTO createUserRequest);
		bool UpdateUser(User user, EditUserDTO updateUserRequest);
	}
}

