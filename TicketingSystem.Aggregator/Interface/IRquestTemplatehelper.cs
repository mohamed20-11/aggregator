using DynamicWorkflow.Aggregator.Dtos;

namespace DynamicWorkflow.Aggregator.Interface
{
    public interface IRquestTemplatehelper
    {
        public List<GetRequestStepDataDto> GetStepsData(List<GetRequestStepDataDto> requestSteps);

    }
}
