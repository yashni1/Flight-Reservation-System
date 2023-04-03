namespace FlightReservationSystem.Dto
{
	public class BookingDto
	{
		public int Price { get; set; }
		public string? ExpiryDate { get; set; }
		public string? CardHolderName { get; set; }
		public long CardNumber { get; set; }
		public int NoOfSeats { get; set; }
		public int ScheduleId { get; set; }
	}
}
