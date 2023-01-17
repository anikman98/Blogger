using Blogger.Data;
using Blogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Data.Repository
{
    public class Repository : IRepository
    {

        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
