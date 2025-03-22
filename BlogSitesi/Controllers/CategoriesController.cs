using BlogSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogSitesi.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            var model = _databaseContext.Categories.FirstOrDefault(c => c.Id == id);
            if (model == null)
            {
                return NotFound("İlgili Post Bulunamadı!");
            }
            return View(model);
        }
    }
}
