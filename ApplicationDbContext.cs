using FlightReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationSystem.DataLayer
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Flight> Flights { get; set; }
		public DbSet<FlightSchedule> Schedules { get; set; }
		public DbSet<Booking> Bookings { get; set; }
	}
}
