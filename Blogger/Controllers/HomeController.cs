using Blog.Data;
using Blog.Models;
using Blogger.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        { 
            return View(); 
        }
        public IActionResult Post()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit() 
        { 
            return View(new Post()); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _repo.AddPost(post);
            if(await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View(post);
        }
    }

}