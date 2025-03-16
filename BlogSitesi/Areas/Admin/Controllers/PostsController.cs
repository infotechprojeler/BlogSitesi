using BlogSitesi.Data;
using BlogSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PostsController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public PostsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: PostsController
        public ActionResult Index()
        {
            return View(_databaseContext.Posts.Include(c => c.Category));
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            var model = _databaseContext.Posts.Include(c => c.Category).FirstOrDefault(p => p.Id == id); // Include kullandırğımızda Find metodu kullanamıyoruz FirstOrDefault u kullanmamız gerekli.
            return View(model);
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories, "Id", "Name");
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post collection, IFormFile? Image)
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
                        collection.Image = Image.FileName;
                    }
                    collection.CreateDate = DateTime.Now;
                    _databaseContext.Posts.Add(collection);
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!" + hata.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories, "Id", "Name");
            return View(collection);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories, "Id", "Name");
            var model = _databaseContext.Posts.Find(id);
            return View(model);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post collection, IFormFile? Image)
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
                        collection.Image = Image.FileName;
                    }
                    _databaseContext.Posts.Update(collection);
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!" + hata.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories, "Id", "Name");
            return View(collection);
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            //var model = _databaseContext.Posts.Find(id); // Include çalışmıyor
            var model = _databaseContext.Posts.Include(c => c.Category).FirstOrDefault(p => p.Id == id);
            return View(model);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post collection)
        {
            try
            {
                _databaseContext.Posts.Remove(collection);
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
