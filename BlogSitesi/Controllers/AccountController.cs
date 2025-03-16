using BlogSitesi.Data;
using BlogSitesi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogSitesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AccountController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            // Kullanıcının UserData değerini almak için:
            var userData = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.UserData)?.Value;

            if (!string.IsNullOrEmpty(userData))
            {
                // userData ile işlem yapabilirsiniz.
                ViewBag.Id = $"UserData: {userData}";
                try
                {
                    var model = _databaseContext.Users.FirstOrDefault(x => x.Id == int.Parse(userData));
                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }                
            }
            else
            {
                ViewBag.Id = "UserData bulunamadı.";
            }
            return View();
        }
        [Authorize, HttpPost]
        public IActionResult Index(User user)
        {
            // Kullanıcının UserData değerini almak için:
            var userData = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.UserData)?.Value;

            if (!string.IsNullOrEmpty(userData))
            {
                // userData ile işlem yapabilirsiniz.
                ViewBag.Id = $"UserData: {userData}";
                try
                {
                    var model = _databaseContext.Users.FirstOrDefault(x => x.Id == int.Parse(userData));
                    if (model is not null)
                    {
                        model.Email = user.Email;
                        model.Password = user.Password;
                        model.Name = user.Name;
                        model.Surname = user.Surname;
                        model.Username = user.Username;
                        _databaseContext.SaveChanges();
                    }
                    
                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }                
            }
            else
            {
                ViewBag.Id = "UserData bulunamadı.";
            }
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = _databaseContext.Users.FirstOrDefault(u => u.IsActive && u.Email == loginViewModel.Email && u.Password == loginViewModel.Password);
                    if (kullanici != null)
                    {
                        var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Email, kullanici.Email), // claim = hak(kullanıcıya tanımlalan haklar)
                        new(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User"),
                        new(ClaimTypes.UserData, kullanici.Id.ToString())
                    };
                        // Adım 2
                        var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcı için bir kimlik oluşturduk
                        // Adım 3
                        ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); // üstte tanımladığımız kullaniciKimligi nesnesindeki bilgilerle yetki prensipleri-kuralları gibi bir nesne daha oluşturuyoruz.
                        // Adım 4
                        HttpContext.SignInAsync(claimsPrincipal).Wait(); // yukardaki yetkilerle sisteme giriş yaptık
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu! Giriş Başarısız!");
                }
            }
            return View(loginViewModel);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.CreateDate = DateTime.Now;
                    user.IsActive = false;
                    user.IsAdmin = false;
                    await _databaseContext.Users.AddAsync(user);
                    var sonuc = await _databaseContext.SaveChangesAsync();
                    if (sonuc > 0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Başarılı!</strong> Üyeliğiniz Onaylandıktan Sonra Sisteme Giriş Yapabilirsiniz.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        return RedirectToAction("Login");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(user);
        }
    }
}
