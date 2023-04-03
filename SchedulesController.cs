using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationSystem.DataLayer;
using FlightReservationSystem.Models;
using FlightReservationSystem.Dto;
using System.Dynamic;

namespace FlightReservationSystem.Controllers
{
	public class SchedulesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SchedulesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Schedules
		public async Task<IActionResult> Index()
		{
			ViewBag.Nav = "Admin";
			return View(await _context.Schedules.Include(u => u.Flight).ToListAsync());
		}

		// GET: Schedules/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			ViewBag.Nav = "Admin";
			if (id == null || _context.Schedules == null)
			{
				return NotFound();
			}

			var flightSchedule = await _context.Schedules.Include(i => i.Flight).FirstOrDefaultAsync(m => m.Id == id);
			if (flightSchedule == null)
			{
				return NotFound();
			}

			return View(flightSchedule);
		}

		// GET: Schedules/Create
		public async Task<ActionResult> Create()
		{
			ViewBag.Nav = "Admin";
			var flights = await _context.Flights.ToListAsync();
			return View(flights);
		}

		// POST: Schedules/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] ScheduleDto dto)
		{
			ViewBag.Nav = "Admin";
			var flight = await _context.Flights.FindAsync(dto.FlightId);
			var flightSchedule = new FlightSchedule()
			{
				DepartureTime = dto.DepatureTime,
				ArrivalTime = dto.ArrivalTime,
				Flight = flight,
				Source = dto.Source,
				Destination = dto.Destination
			};
			_context.Add(flightSchedule);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Schedules/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			ViewBag.Nav = "Admin";
			if (id == null || _context.Schedules == null)
			{
				return NotFound();
			}

			var flightSchedule = await _context.Schedules.Include(i => i.Flight).SingleOrDefaultAsync(i => i.Id == id);
			if (flightSchedule == null)
			{
				return NotFound();
			}
			dynamic data = new ExpandoObject();
			data.FlightSchedule = flightSchedule;
			data.Flights = await _context.Flights.ToListAsync();
			return View(data);
		}

		// POST: Schedules/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, IFormCollection formData)
		{
			var flightSchedule = await _context.Schedules.FindAsync(id);
			ViewBag.Nav = "Admin";
			if (flightSchedule == null)
			{
				return NotFound();
			}
			try
			{
				if (formData == null)
				{
					return BadRequest();
				}
				int flightId = Convert.ToInt32(formData["FlightId"]!);
				var flight = await _context.Flights.FindAsync(flightId);
				flightSchedule.Flight = flight;
				flightSchedule.ArrivalTime = DateTime.Parse(formData["ArrivalTime"]!);
				flightSchedule.DepartureTime = DateTime.Parse(formData["DepatureTime"]!);
				flightSchedule.Source = formData["Source"]!;
				flightSchedule.Destination = formData["Destination"]!;
				_context.Update(flightSchedule);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FlightScheduleExists(flightSchedule.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));
		}

		// GET: Schedules/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			ViewBag.Nav = "Admin";
			if (id == null || _context.Schedules == null)
			{
				return NotFound();
			}

			var flightSchedule = await _context.Schedules
				.Include(u => u.Flight)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (flightSchedule == null)
			{
				return NotFound();
			}

			return View(flightSchedule);
		}

		// POST: Schedules/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			ViewBag.Nav = "Admin";
			if (_context.Schedules == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Schedules'  is null.");
			}
			var flightSchedule = await _context.Schedules.FindAsync(id);
			if (flightSchedule != null)
			{
				_context.Schedules.Remove(flightSchedule);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool FlightScheduleExists(int id)
		{
			return _context.Schedules.Any(e => e.Id == id);
		}
	}
}
