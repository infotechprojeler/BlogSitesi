using BlogSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogSitesi.ViewComponents
{
    public class Kategoriler : ViewComponent
    {
        private readonly DatabaseContext _databaseContext;

        public Kategoriler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IViewComponentResult Invoke(string secili)
        {
            ViewBag.Secili = secili;
            return View(_databaseContext.Categories.Where(c => c.IsActive == true));
        }
    }
}
