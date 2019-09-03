using System.Collections.Generic;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;

namespace BlogDemo.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        void AddPost(Post post);
    }
}