namespace FlightReservationSystem.Dto
{
	public class ScheduleDto
	{
		public int FlightId { get; set; }
		public DateTime ArrivalTime { get; set; }
		public DateTime DepatureTime { get; set; }
		public string? Source { get; set; }
		public string? Destination { get; set; }
	}
}
