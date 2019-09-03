using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interfaces;
using BlogDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyContext _myContext;

        public PostRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _myContext.Posts.ToListAsync();
        }

        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }
    }
}
