using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interfaces;
using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly MyContext _myContext;

        public PostController(MyContext myContext)
        {
            _myContext = myContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _myContext.Posts.ToListAsync();

            return Ok(posts);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post()
        //{
        //    var newPost = new Post
        //    {
        //        Author = "admin",
        //        Body = "1231321312312321312321321",
        //        Title = "Title A",
        //        LastModified = DateTime.Now
        //    };

        //    _postRepository.AddPost(newPost);

        //    await _unitOfWork.SaveAsync();

        //    return Ok();
        //}
    }
}
