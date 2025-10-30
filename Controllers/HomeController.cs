using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSeat.Data;
using SecureSeat.Models;
using Microsoft.AspNetCore.Authorization;

namespace SecureSeat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _context.Shows
                .OrderBy(e => e.Date)
                .Take(3)
                .ToListAsync();

                return View(events);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error loading events in Home/Index");

                
                return View(new List<Show>());

            }
        }

      
        public IActionResult Event()
        {
            return View();
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
