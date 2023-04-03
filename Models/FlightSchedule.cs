using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationSystem.Models
{
	public class FlightSchedule
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? Source { get; set; }

		[Required]
		public string? Destination { get; set; }

		[Required]
		public DateTime DepartureTime { get; set; }

		[Required]
		public DateTime ArrivalTime { get; set; }

		//one to one mapping for the flight that is scheduled
		public Flight? Flight { get; set; }
	}
}
