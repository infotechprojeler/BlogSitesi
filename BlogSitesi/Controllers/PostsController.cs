using BlogSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.Controllers
{
    public class PostsController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public PostsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index(string q = "")
        {
            return View(_databaseContext.Posts.Where(p => p.IsActive && p.Title.Contains(q) || p.Content.Contains(q)));
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _databaseContext.Posts.Where(p => p.IsActive && p.Id == id).Include(c => c.Category).FirstOrDefaultAsync();
            return View(model);
        }
    }
}
