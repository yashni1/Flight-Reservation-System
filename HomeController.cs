using FlightReservationSystem.Dto;
using FlightReservationSystem.DataLayer;
using FlightReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FlightReservationSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext db;

		public HomeController(ApplicationDbContext db)
		{
			this.db = db;
		}

		public IActionResult Index()
		{
			ViewBag.Nav = "Home";
			return View();
		}

		public IActionResult Login() //asp-action
		{
			ViewBag.Nav = "Home";
			return View();
		}

		public IActionResult Register()
		{
			ViewBag.Nav = "Home";
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginDto loginDetails)
		{
			ViewBag.Nav = "Home";
			var user = await db.Users.Where(u => u.Email!.Equals(loginDetails.Email)).FirstOrDefaultAsync();

			if (user == null)
			{
				ViewData["error"] = "User not found,please register";
				return View();
			}
			bool verified = BCrypt.Net.BCrypt.Verify(loginDetails.Password, user.Password); // verifying the password againest the hashed password
			if (verified)
			{
				if (loginDetails.UserType!.Equals("ADMIN") && user.IsAdmin)
				{
					HttpContext.Session.SetInt32("user", user.Id);
					user.LoginStatus = true;
					db.Users.Update(user);
					await db.SaveChangesAsync();
					return RedirectToAction("Home", "Admin");
				}
				else
				{
					if (user.LoginStatus)
					{
						ViewData["error"] = "You are logged in somewhere please log out there and log in here";
						return View();
					}
					else
					{
						HttpContext.Session.SetInt32("user", user.Id);
						user.LoginStatus = true;
						db.Users.Update(user);
						await db.SaveChangesAsync();
						return RedirectToAction("Home", "User");
					}
				}
			}
			else
			{
				ViewData["error"] = "Passwords Doesn't Match";
				return View();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(User user)
		{
			ViewBag.Nav = "Home";
			var existingUser = await db.Users.Where(u => u.Email!.Equals(user.Email)).FirstOrDefaultAsync();

			if (existingUser != null)
			{
				ViewData["error"] = "User Exists, Please Login";
				return View();
			}
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // hashing the password so that it cannot be understood when seen
			await db.Users.AddAsync(user);
			await db.SaveChangesAsync();
			ViewData["msg"] = "User Registered Successfully";
			return View();
		}

		public async Task<ActionResult> Logout()
		{
			var userId = HttpContext.Session.GetInt32("user");
			if (userId != null && userId > 0)
			{
				var user = await db.Users.FindAsync(userId);

				if (user == null)
				{
					return NotFound();
				}
				user.LoginStatus = false;
				db.Users.Update(user);
				await db.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}
			return NotFound();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}