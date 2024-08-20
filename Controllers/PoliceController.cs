using DalbudakSigorta.Data;
using DalbudakSigorta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DalbudakSigorta.Controllers
{
    public class PoliceController : Controller
    {

        private readonly DataContext _context;

        public PoliceController(DataContext context)
        {
            _context = context;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var policeKayitlari = await _context
                                .Policeler
                                .Include(p => p.Musteri)
                                .ToListAsync();
            return View(policeKayitlari);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var police = await _context.Policeler
                .Include(p => p.Musteri)  // Include Musteri
                .Include(p => p.AracKayit) // Include AracKayit
                .FirstOrDefaultAsync(p => p.PoliceNo == id);

            if (police == null)
            {
                return NotFound();
            }

            var viewModel = new PoliceEditViewModel
            {
                Police = police,
                AracKayit = police.AracKayit ?? new AracKayit() // Handle null AracKayit
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PoliceEditViewModel viewModel)
        {
            if (viewModel.Police == null || id != viewModel.Police.PoliceNo)
            {
                return NotFound();
            }

            // Retrieve the police record to update
            var policeToUpdate = await _context.Policeler
                .Include(p => p.Musteri)
                .Include(p => p.AracKayit)
                .FirstOrDefaultAsync(p => p.PoliceNo == id);

            if (policeToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the status is being changed from "T" to "P"
                if (policeToUpdate.Status == "T" && viewModel.Police.Status == "P")
                {
                    // Update the status in the database first
                    policeToUpdate.Status = viewModel.Police.Status;

                    _context.Update(policeToUpdate);
                    await _context.SaveChangesAsync();

                    // Redirect to the Payment action
                    return RedirectToAction("Payment", new { policeNo = id });
                }
                else
                {
                    // If the status is not changing from "T" to "P", just update the status
                    policeToUpdate.Status = viewModel.Police.Status;

                    _context.Update(policeToUpdate);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            // If something failed, reload data and return to the view
            viewModel.Police = policeToUpdate;
            viewModel.AracKayit = policeToUpdate.AracKayit ?? new AracKayit();
            return View(viewModel);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Musteriler = new SelectList(await _context.Musteriler.ToListAsync(), "MusteriId", "AdSoyad");
            ViewBag.BransKodlari = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "310", Text = "Trafik Kasko" },
                new SelectListItem { Value = "610", Text = "Sağlık Sigortası" }
            }, "Value", "Text");

            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string bransKodu, int musteriId)
        {
            return RedirectToAction("CreateOffer", new { bransKodu = bransKodu, musteriId = musteriId });
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateOffer(string bransKodu, int musteriId)
        {
            var model = new CreateOfferViewModel
            {
                BransKodu = bransKodu,
                MusteriId = musteriId,
                IsOfferGenerated = false // Initialize IsOfferGenerated to false
            };

            ViewBag.BransKodu = bransKodu;
            ViewBag.MusteriId = musteriId;
            ViewBag.MarkaList = new SelectList(await _context.Araclar.Select(a => a.AracMarka).Distinct().ToListAsync());
            ViewBag.ModelYiliList = new SelectList(await _context.Araclar.Select(a => a.AracModelYili).Distinct().ToListAsync());

            return View(model);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOffer(CreateOfferViewModel model, string action)
        {
            if (ModelState.IsValid)
            {
                if (action == "TeklifAl")
                {
                    var arac = await _context.Araclar
                        .FirstOrDefaultAsync(a => a.PlakaIlKodu == model.PlakaIlKodu &&
                                                  a.PlakaKodu == model.PlakaKodu &&
                                                  a.AracMarka == model.AracMarka &&
                                                  a.AracModel == model.AracModel &&
                                                  a.AracModelYili == model.AracModelYili);

                    if (arac != null)
                    {
                        model.KaskoDegeri = arac.KaskoDegeri;
                        model.TeklifTutari = arac.KaskoDegeri * 0.01m; // 1% of KaskoDegeri
                        model.IsOfferGenerated = true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Girilen araç bilgileri doğru değil.");
                        model.IsOfferGenerated = false;
                    }
                }
                else if (action == "Teklifleştir")
                {
                    if (model.IsOfferGenerated)
                    {
                        int policeNo = await GenerateUniquePoliceNoAsync();

                        var police = new Police
                        {
                            PoliceNo = policeNo,
                            BransKodu = model.BransKodu,
                            MusteriId = model.MusteriId,
                            Prim = model.TeklifTutari,
                            TanzimTarihi = DateTime.Today,
                            BaslangicTarihi = DateTime.Today,
                            BitisTarihi = DateTime.Today.AddYears(1),
                            Status = "T",  // Set status to "T" (Teklif)
                            KullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")
                        };

                        _context.Policeler.Add(police);
                        await _context.SaveChangesAsync();

                        var arac = await _context.Araclar
                            .FirstOrDefaultAsync(a => a.PlakaIlKodu == model.PlakaIlKodu &&
                                                      a.PlakaKodu == model.PlakaKodu &&
                                                      a.AracMarka == model.AracMarka &&
                                                      a.AracModel == model.AracModel &&
                                                      a.AracModelYili == model.AracModelYili);

                        if (arac != null)
                        {
                            var aracKayit = new AracKayit
                            {
                                PoliceNo = policeNo,
                                PlakaIlKodu = model.PlakaIlKodu,
                                PlakaKodu = model.PlakaKodu,
                                AracMarka = model.AracMarka,
                                AracModel = model.AracModel,
                                AracModelYili = model.AracModelYili,
                                MotorNo = arac.MotorNo,
                                SasiNo = arac.SasiNo
                            };

                            _context.AracKayitlari.Add(aracKayit);
                            await _context.SaveChangesAsync();
                        }

                        // Instead of redirecting, return to a relevant page like Index or another view
                        return RedirectToAction("Index", "Police"); // Change "Police" to your desired controller or action
                    }
                    else
                    {
                        ModelState.AddModelError("", "Önce teklif almanız gerekmektedir.");
                    }
                }
                else if (action == "Policelestir")
                {
                    if (model.IsOfferGenerated)
                    {
                        int policeNo = await GenerateUniquePoliceNoAsync();

                        var police = new Police
                        {
                            PoliceNo = policeNo,
                            BransKodu = model.BransKodu,
                            MusteriId = model.MusteriId,
                            Prim = model.TeklifTutari,
                            TanzimTarihi = DateTime.Today,
                            BaslangicTarihi = DateTime.Today,
                            BitisTarihi = DateTime.Today.AddYears(1),
                            Status = "T",  // Initially set status to "T" (Teklif)
                            KullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")
                        };

                        _context.Policeler.Add(police);
                        await _context.SaveChangesAsync();

                        var arac = await _context.Araclar
                            .FirstOrDefaultAsync(a => a.PlakaIlKodu == model.PlakaIlKodu &&
                                                      a.PlakaKodu == model.PlakaKodu &&
                                                      a.AracMarka == model.AracMarka &&
                                                      a.AracModel == model.AracModel &&
                                                      a.AracModelYili == model.AracModelYili);

                        if (arac != null)
                        {
                            var aracKayit = new AracKayit
                            {
                                PoliceNo = policeNo,
                                PlakaIlKodu = model.PlakaIlKodu,
                                PlakaKodu = model.PlakaKodu,
                                AracMarka = model.AracMarka,
                                AracModel = model.AracModel,
                                AracModelYili = model.AracModelYili,
                                MotorNo = arac.MotorNo,
                                SasiNo = arac.SasiNo
                            };

                            _context.AracKayitlari.Add(aracKayit);
                            await _context.SaveChangesAsync();
                        }

                        // Redirect to the payment page with the generated PoliceNo
                        return RedirectToAction("Payment", new { policeNo = police.PoliceNo });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Önce teklif almanız gerekmektedir.");
                    }
                }
            }

            // Ensure ViewBag is populated correctly
            ViewBag.MarkaList = new SelectList(await _context.Araclar.Select(a => a.AracMarka).Distinct().ToListAsync());
            ViewBag.ModelList = await _context.Araclar
                                .Where(a => a.AracMarka == model.AracMarka)
                                .Select(a => a.AracModel)
                                .Distinct()
                                .ToListAsync();

            ViewBag.ModelYiliList = await _context.Araclar
                                .Where(a => a.AracMarka == model.AracMarka && a.AracModel == model.AracModel)
                                .Select(a => a.AracModelYili)
                                .Distinct()
                                .ToListAsync();

            return View("CreateOffer", model);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Payment(int policeNo)
        {
            var police = await _context.Policeler.FirstOrDefaultAsync(p => p.PoliceNo == policeNo);
            if (police == null)
            {
                return NotFound();
            }

            var model = new PaymentViewModel
            {
                PoliceNo = police.PoliceNo,
                OdemeTutari = (int)police.Prim, // Set the payment amount to the police's premium
                                                // Additional fields can be set as required
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var odemeBilgisi = new OdemeBilgisi
                {
                    PoliceNo = model.PoliceNo,
                    OdemeTutari = model.OdemeTutari,
                    OdemeTarihi = DateTime.Now,
                    KrediKartiNo = model.KrediKartiNo,
                    KartIsimSoyisim = model.KartIsimSoyisim,
                    SonKullanmaAy = model.SonKullanmaAy,
                    SonKullanmaYil = model.SonKullanmaYil,
                    CVV = model.CVV
                };

                _context.OdemeBilgileri.Add(odemeBilgisi);
                await _context.SaveChangesAsync();

                // Update the Police status to "P" after successful payment
                var policeToUpdate = await _context.Policeler.FindAsync(model.PoliceNo);
                if (policeToUpdate != null)
                {
                    policeToUpdate.Status = "P";
                    _context.Update(policeToUpdate);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            // If the payment fails, reload the payment page
            return View(model);
        }


        private async Task<IActionResult> PolicelestirAsync(CreateOfferViewModel model, string status)
        {
            var police = new Police
            {
                PoliceNo = model.PoliceNo,
                BransKodu = model.BransKodu,
                MusteriId = model.MusteriId,
                Prim = model.TeklifTutari,
                TanzimTarihi = DateTime.Today,
                BaslangicTarihi = DateTime.Today,
                BitisTarihi = DateTime.Today.AddYears(1),
                Status = status,
                KullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")
            };

            _context.Policeler.Add(police);
            await _context.SaveChangesAsync();

            var arac = await _context.Araclar
                .FirstOrDefaultAsync(a => a.PlakaIlKodu == model.PlakaIlKodu &&
                                        a.PlakaKodu == model.PlakaKodu &&
                                        a.AracMarka == model.AracMarka &&
                                        a.AracModel == model.AracModel &&
                                        a.AracModelYili == model.AracModelYili);

            if (arac != null)
            {
                var aracKayit = new AracKayit
                {
                    PoliceNo = police.PoliceNo,
                    PlakaIlKodu = model.PlakaIlKodu,
                    PlakaKodu = model.PlakaKodu,
                    AracMarka = model.AracMarka,
                    AracModel = model.AracModel,
                    AracModelYili = model.AracModelYili,
                    MotorNo = arac.MotorNo,
                    SasiNo = arac.SasiNo
                };

                _context.AracKayitlari.Add(aracKayit);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        private async Task<int> GenerateUniquePoliceNoAsync()
        {
            int policeNo;
            do
            {
                policeNo = new Random().Next(10000000, 99999999);
            } while (await _context.Policeler.AnyAsync(p => p.PoliceNo == policeNo));
            return policeNo;
        }



        [HttpGet]
        public async Task<IActionResult> GetModelsByMarka(string marka)
        {
            var models = await _context.Araclar
                .Where(a => a.AracMarka == marka)
                .Select(a => a.AracModel)
                .Distinct()
                .ToListAsync();

            return Json(models);
        }


        [HttpGet]
        public async Task<IActionResult> GetModelYearsByMarkaAndModel(string marka, string model)
        {
            var years = await _context.Araclar
                .Where(a => a.AracMarka == marka && a.AracModel == model)
                .Select(a => a.AracModelYili)
                .Distinct()
                .ToListAsync();

            return Json(years);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var police = await _context.Policeler
                                       .Include(p => p.Musteri)
                                       .Include(p => p.AracKayit)
                                       .FirstOrDefaultAsync(p => p.PoliceNo == id);

            if (police == null)
            {
                return NotFound();
            }

            return View(police);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var police = await _context.Policeler
                                       .Include(p => p.AracKayit)
                                       .Include(p => p.OdemeBilgisi)  // Include the payment information
                                       .FirstOrDefaultAsync(p => p.PoliceNo == id);
            if (police == null)
            {
                return NotFound();
            }

            // Delete the associated OdemeBilgisi record if it exists
            if (police.OdemeBilgisi != null)
            {
                _context.OdemeBilgileri.Remove(police.OdemeBilgisi);
            }

            // Delete the associated AracKayit record if it exists
            if (police.AracKayit != null)
            {
                _context.AracKayitlari.Remove(police.AracKayit);
            }

            // Delete the Police record
            _context.Policeler.Remove(police);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult TestPayment()
        {
            return RedirectToAction("Payment", new { policeNo = 47192764 }); // Use a known PoliceNo
        }


    }
}