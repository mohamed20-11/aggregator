namespace DynamicWorkflow.Aggregator.IGrpcServices
{
    public interface IEscalationLevel
    {
        public Task<ResponseDTO> GetAllEscalationLevels(int pageSize, int pageNumber);
        public Task<ResponseDTO> GetEscalationLevelById(Guid escalationLevelId);
    }
}
