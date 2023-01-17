using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);

        Task<bool> SaveChangesAsync();
    }
}
