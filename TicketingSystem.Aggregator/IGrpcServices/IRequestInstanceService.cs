namespace DynamicWorkflow.Aggregator.IGrpcServices
{
    public interface IRequestInstanceService
    {
        public Task<ResponseDTO> GetRequestInstanceById(Guid requestInstanceId);
    }
}
