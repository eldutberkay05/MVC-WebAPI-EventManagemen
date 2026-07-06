using EventReservationMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.VenueCount = _context.Venues.Count();
            ViewBag.EventCount = _context.Events.Count();

            var events = _context.Events
                .Include(x => x.Category)
                .Include(x => x.Venue)
                .OrderByDescending(x => x.EventDate)
                .ToList();

            return View(events);
        }
    }
}