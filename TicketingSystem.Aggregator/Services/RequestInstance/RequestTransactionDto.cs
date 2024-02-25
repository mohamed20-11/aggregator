namespace DynamicWorkflow.Aggregator.Services.RequestInstance
{
    public class RequestTransactionDto
    {
        public string? Comment { get; set; }
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime AcceptanceDate { get; set; }
    }
}
