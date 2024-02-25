namespace TicketingSystem.Aggregator.Dtos
{
	public class EscalationLevelDto
	{
		public Guid? Id { get; set; }
		public string EscalationLevelName { get; set; }
		public int? LevelIndex { get; set; }
		public int? ResponseMinutes { get; set; }
		public int? ResponseHours { get; set; }
		public int? ResponseDays { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string ThirdName { get; set; }
		public string FourthName { get; set; }
		public Guid? SLAId { get; set; }
		public Guid? UserId { get; set; }

	}
}
