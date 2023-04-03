using System.ComponentModel.DataAnnotations;

namespace FlightReservationSystem.Models
{
	public class Flight
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Source { get; set; }
		[Required]
		public string? Destination { get; set; }
		[Required]
		public int EstimatedTravelDuration { get; set; }
		[Required]
		public int SeatingCapacity { get; set; }
		public string? ReservationType { get; set; }
		public int ReservationCapacity { get; set; }
		[Required]
		public int Price { get; set; }
	}
}
