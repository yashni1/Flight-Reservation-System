using FlightReservationSystem.DataLayer;
using FlightReservationSystem.Dto;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationSystem.Controllers
{
	public class UserController : Controller
	{
		private readonly ApplicationDbContext db;

		public UserController(ApplicationDbContext db)
		{
			this.db = db;
		}

		public async Task<ActionResult> Home()
		{
			ViewBag.Nav = "User";
			var schedules = await db.Schedules
				.Include(s => s.Flight)
				.Where(s => s.Flight!.ReservationCapacity > 0)
				.ToListAsync();
			return View(schedules);
		}

		public async Task<ActionResult> Search(string source, string destination)
		{
			ViewBag.Nav = "User";
			var schedules = await db.Schedules
				.Where(s => s.Source!.Equals(source) && s.Destination!.Equals(destination) && s.Flight!.Price > 0)
				.Include(s => s.Flight)
				.ToListAsync();
			return View("Home", schedules);
		}

		public async Task<ActionResult> Book(int id)
		{
			ViewBag.Nav = "User";
			var schedule = await db.Schedules
				.Include(s => s.Flight)
				.FirstOrDefaultAsync(u => u.Id == id);
			return View(schedule);
		}

		public async Task<ActionResult> Cancel(int id)
		{
			ViewBag.Nav = "User";
			var booking = await db.Bookings.FindAsync(id);
			if (booking == null)
			{
				return NotFound();
			}
			booking.Status = "CANCELLED";
			db.Bookings.Update(booking);
			await db.SaveChangesAsync();
			return RedirectToAction(nameof(Bookings));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddBook([FromForm] BookingDto dto)
		{
			ViewBag.Nav = "User";
			var userId = HttpContext.Session.GetInt32("user");
			var user = await db.Users.FindAsync(userId);
			var schedule = await db.Schedules.FindAsync(dto.ScheduleId);
			var booking = new Booking()
			{
				Price = dto.Price,
				NoOfSeats = dto.NoOfSeats,
				ExpiryDate = dto.ExpiryDate,
				CardHolderName = dto.CardHolderName,
				CardNumber = dto.CardNumber,
				User = user,
				FlightSchedule = schedule,
			};
			//schedule!.Flight!.ReservationCapacity -= dto.NoOfSeats;
			await db.Bookings.AddAsync(booking);
			await db.SaveChangesAsync();
			ViewData["msg"] = "Ticket Booked Successfully";
			return RedirectToAction(nameof(Home));
		}

		public async Task<ActionResult> Bookings()
		{
			ViewBag.Nav = "User";
			//get all the bookings of the user in the bookings model and pass it to view
			var userId = HttpContext.Session.GetInt32("user");
			var bookings = await db.Bookings
				.Where(i => i.User!.Id == userId)
				.Include(i => i.User)
				.Include(i => i.FlightSchedule)
				.ToListAsync();
			return View(bookings);
		}
	}
}
