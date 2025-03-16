using BlogSitesi.Data;
using BlogSitesi.Models;
using Microsoft.AspNetCore.Authorization; // güvenlik kütüphanesi
using Microsoft.AspNetCore.Mvc;

namespace BlogSitesi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View(_databaseContext.Categories);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            var model = _databaseContext.Categories.Find(id);
            return View(model);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
                        using var stream = new FileStream(klasor + Image.FileName, FileMode.Create);
                        Image.CopyTo(stream);
                        collection.Image = Image.FileName; // kategorinin içindeki image alanının değeri, yüklenen resmin adına eşitlensin
                    }
                    _databaseContext.Categories.Add(collection);
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _databaseContext.Categories.Find(id);
            return View(model);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
                        using var stream = new FileStream(klasor + Image.FileName, FileMode.Create);
                        Image.CopyTo(stream);
                        collection.Image = Image.FileName; // kategorinin içindeki image alanının değeri, yüklenen resmin adına eşitlensin
                    }
                    _databaseContext.Categories.Update(collection);
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _databaseContext.Categories.Find(id);
            return View(model);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                _databaseContext.Categories.Remove(collection);
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
