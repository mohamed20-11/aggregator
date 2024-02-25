namespace DynamicWorkflow.Aggregator.Services.Requesttemplate
{
    public class GetRequestTemplateByIdDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int PathCount { get; set; }
        public List<GetRequestStepDataDto> requestTemplateStepDtos { get; set; } = new();
    }
}
