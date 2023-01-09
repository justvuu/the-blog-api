using System;
using TheBlogAPI.Models.DTO;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Interface
{
	public interface IPostRepository
	{
		ICollection<Post> GetAll();
        //ICollection<DetailPost> GetDetailPost();
        bool CreatePost(AddPostDTO createPostRequest);
		Post GetBySlug(string slug);
		bool UpdatePost(Guid postId, EditPostDTO updatePostRequest);
		bool DeletePost(Guid postId);
	}
}

