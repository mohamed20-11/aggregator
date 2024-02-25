namespace DynamicWorkflow.Aggregator.Dtos
{
    public class SlaDataResponse
    {
        public string SlaId { get; set; }
        public string SlaName { get; set; }
        public int ImplementDays { get; set; }
        public int ImplementHours { get; set; }
        public int ImplementMinutes { get; set; }
        public int RespondDays { get; set; }
        public int RespondHours { get; set; }
        public int RespondMinutes { get; set; }
    }
}
