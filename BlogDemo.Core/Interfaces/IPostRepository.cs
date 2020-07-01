using System.Collections.Generic;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;

namespace BlogDemo.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<PaginatedList<Post>> GetAllPostsAsync(PostParameters postParameters);
        Task<Post> GetPostByIdAsync(int id);
        void AddPost(Post post);
        void Delete(Post post);
        void Update(Post post);
    }
}