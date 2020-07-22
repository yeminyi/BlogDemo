using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Extensions;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyContext _myContext;
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        public PostRepository(MyContext myContext,
             IPropertyMappingContainer propertyMappingContainer)
        {
            _myContext = myContext;
            _propertyMappingContainer = propertyMappingContainer;
        }
  
        public async Task<PaginatedList<Post>> GetAllPostsAsync(PostParameters postParameters)
        {
            var query = _myContext.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(postParameters.Title))
            {
                var title = postParameters.Title.ToLowerInvariant();
                query = query.Where(x => x.Title.ToLowerInvariant().Contains(title));
            }

            query = query.ApplySort(postParameters.OrderBy, _propertyMappingContainer.Resolve<PostResource, Post>());

            var count = await query.CountAsync();
            var data = await query
                .Skip(postParameters.PageIndex * postParameters.PageSize)
                .Take(postParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<Post>(postParameters.PageIndex, postParameters.PageSize, count, data);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _myContext.Posts.FindAsync(id);
        }
        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }
        public void Delete(Post post)
        {
            _myContext.Posts.Remove(post);
        }
        public void Update(Post post)
        {
            _myContext.Entry(post).State = EntityState.Modified;
        }
    }
}
