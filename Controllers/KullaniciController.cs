using DalbudakSigorta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DalbudakSigorta.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace DalbudakSigorta.Controllers
{
    public class KullaniciController : Controller
    {

        private readonly DataContext _context;

        public KullaniciController(DataContext context)
        {
            _context = context;
        }

        // public IQueryable<Kullanici> Kullanicilar => _context.Kullanicilar;
        // public void CreateUser(Kullanici kullanici)
        // {
        //     _context.Kullanicilar.Add(kullanici);
        //     _context.SaveChanges();
        // }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var kullanicilar = await _context.Kullanicilar.ToListAsync();
            return View(kullanicilar);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var user = await _context.Kullanicilar
                    .FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Eposta == model.Eposta);

                if (user == null)
                {
                    // Create a new user
                    var newUser = new Kullanici
                    {
                        UserName = model.UserName,
                        Eposta = model.Eposta,
                        Password = model.Password, // Password hashing should be implemented
                        KullaniciAd = model.KullaniciAd,
                        KullaniciSoyad = model.KullaniciSoyad,
                        BaslamaTarihi = DateTime.Now
                    };

                    // Add the new user to the context
                    _context.Kullanicilar.Add(newUser);
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Email is already in use.");
                }
            }

            return View(model);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullan = await _context.Kullanicilar
                .Include(k => k.Araclar)
                .Include(k => k.Policeler)  // Include Policeler based on OnaylayanId
                .FirstOrDefaultAsync(k => k.KullaniciId == id);

            if (kullan == null)
            {
                return NotFound();
            }

            return View(kullan);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kullanici model)
        {
            if (id != model.KullaniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kullanicilar.Any(o => o.KullaniciId == model.KullaniciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanicilar.FindAsync(id);

            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }
            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var user = await _context.Kullanicilar
                    .FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Eposta == model.Eposta);

                if (user == null)
                {
                    // Create a new user
                    var newUser = new Kullanici
                    {
                        UserName = model.UserName,
                        Eposta = model.Eposta,
                        Password = model.Password, // Currently, password hashing is not implemented.
                        KullaniciAd = model.KullaniciAd,
                        KullaniciSoyad = model.KullaniciSoyad,
                        BaslamaTarihi = DateTime.Now
                    };

                    // Add the new user to the context
                    _context.Kullanicilar.Add(newUser);
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Email is already in use.");
                }
            }

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var isUser = _context.Kullanicilar.FirstOrDefault(x => x.Eposta == model.Eposta && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.KullaniciId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));

                    if (isUser.Eposta == "admin@adayazilim.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

    }
}