using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationSystem.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int Price { get; set; }

		public FlightSchedule? FlightSchedule { get; set; }

		[Required]
		public int? NoOfSeats { get; set; }

		public User? User { get; set; }

		public DateTime Created { get; set; } = DateTime.Now;

		public string? Status { get; set; } = "BOOKED";
		[Required]
		[StringLength(50)]
		public string? CardHolderName { get; set; }
		[Required]
		public long CardNumber { get; set; }
		[Required]
		public string? ExpiryDate { get; set; }
	}
}
