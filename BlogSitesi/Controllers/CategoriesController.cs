using BlogSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            // var model = _databaseContext.Categories.Include(x => x.Posts).FirstOrDefault(c => c.Id == id); // kategorilere postları dahil edip sonra id ye göre eşleştirme yapıyor.

            // var model = _databaseContext.Categories.Where(c => c.Id == id).Include(x => x.Posts).FirstOrDefault(); // id ile eşleşen kategoriyi seçip ona postlarını dahil edip bu kaydı getiriyor

            var model = await _databaseContext.Categories.Where(c => c.Id == id).Include(x => x.Posts).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound("İlgili Post Bulunamadı!");
            }
            return View(model);
        }
    }
}
