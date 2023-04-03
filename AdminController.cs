using FlightReservationSystem.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationSystem.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext db;

		public AdminController(ApplicationDbContext db)
		{
			this.db = db;
		}

		public IActionResult Home()
		{
			ViewBag.Nav = "Admin";
			return View();
		}

		public async Task<ActionResult> Bookings()
		{
			ViewBag.Nav = "Admin";
			var bookings = await db.Bookings.ToListAsync();
			return View(bookings);
		}

	}
}
