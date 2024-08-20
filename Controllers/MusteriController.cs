using DalbudakSigorta.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DalbudakSigorta.Controllers
{
    public class MusteriController : Controller
    {

        private readonly DataContext _context;

        public MusteriController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var musteriler = await _context.Musteriler.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.searchString = searchString;
                musteriler = musteriler.Where(p =>
                    (p.TCKimlik?.Contains(searchString) ?? false) ||
                    (p.AdSoyad?.ToLower().Contains(searchString) ?? false)
                ).ToList();
            }

            return View(musteriler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Musteri model)
        {
            if (ModelState.IsValid)
            {
                _context.Musteriler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
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

            var musteri = await _context.Musteriler
                                        .Include(m => m.Policeler) // Ensure Policeler is loaded
                                        .FirstOrDefaultAsync(m => m.MusteriId == id);

            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // data açığı için önemli, kullanıcı token,'nin integrity'si için //Formu yükleyen kişi ile güncelleyen kişi aynı mı diye kontrol ettiğimiz token.
        public async Task<IActionResult> Edit(int id, Musteri model)
        {
            if (id != model.MusteriId)
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
                    if (!_context.Musteriler.Any(o => o.MusteriId == model.MusteriId))
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

            var musteri = await _context.Musteriler.FindAsync(id);

            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var musteri = await _context.Musteriler.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            _context.Musteriler.Remove(musteri);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}