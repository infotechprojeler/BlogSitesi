using BlogSitesi.Data;
using BlogSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSitesi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public UsersController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            return View(_databaseContext.Users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var model = _databaseContext.Users.Find(id);
            return View(model);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseContext.Users.Add(collection);
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _databaseContext.Users.FirstOrDefault(u => u.Id == id);
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseContext.Users.Update(collection);
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

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _databaseContext.Users.Find(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User collection)
        {
            try
            {
                _databaseContext.Users.Remove(collection);
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
