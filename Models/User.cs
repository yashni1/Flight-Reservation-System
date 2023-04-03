using System.ComponentModel.DataAnnotations;

namespace FlightReservationSystem.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int Age { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Password { get; set; }
		public string? Phone { get; set; }
		public string? Gender { get; set; }
		public string? Dob { get; set; }
		public string? Address { get; set; }
		public bool LoginStatus { get; set; } = false;
		public bool IsAdmin { get; set; } = false;
	}
}
