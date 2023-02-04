using Blogger.Data.FileManager;
using Blogger.Data.Repository;
using Blogger.Models;
using Blogger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Blogger.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;
        public PanelController(IFileManager fileManager, IRepository repo) 
        {
            _fileManager = fileManager;
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                post.Image = await _fileManager.SaveImage(vm.Image);
                

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View(post);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.DeletePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}