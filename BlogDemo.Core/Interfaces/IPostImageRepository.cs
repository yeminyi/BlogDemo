using BlogDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Interfaces
{
    public interface IPostImageRepository
    {
        void Add(PostImage postImage);
    }
}
