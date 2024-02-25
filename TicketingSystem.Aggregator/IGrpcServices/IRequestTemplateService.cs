namespace DynamicWorkflow.Aggregator.IGrpcServices
{
    public interface IRequestTemplateService
    {
        Task<ResponseDTO> GetRequestTemplateById(Guid RequestTemplateId);
    }
}
