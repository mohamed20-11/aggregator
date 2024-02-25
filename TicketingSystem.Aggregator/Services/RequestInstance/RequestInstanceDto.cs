namespace DynamicWorkflow.Aggregator.Services.RequestInstance
{
    public class RequestInstanceDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? RequesterName { get; set; }
        public DateTime RequestDate { get; set; }
        public List<RequestTransactionDto> Transactions { get; set; } = new List<RequestTransactionDto>();
    }
}
