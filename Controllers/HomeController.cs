using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DalbudakSigorta.Models;
using DalbudakSigorta.Data;
using Microsoft.EntityFrameworkCore;

namespace DalbudakSigorta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestPoliceler = await _context.Policeler
                .Include(p => p.Musteri)
                .OrderByDescending(p => p.TanzimTarihi)
                .Take(5)
                .ToListAsync();

            var latestMusteriler = await _context.Musteriler
                .OrderByDescending(m => m.MusteriId)
                .Take(5)
                .ToListAsync();

            var model = new HomeViewModel
            {
                LatestPoliceler = latestPoliceler,
                LatestMusteriler = latestMusteriler
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
