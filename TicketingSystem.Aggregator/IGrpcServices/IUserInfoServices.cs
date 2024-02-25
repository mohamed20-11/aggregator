namespace DynamicWorkflow.Aggregator.IGrpcServices
{
    public interface IUserInfoService
    {
        public Task<ResponseDTO> GetUserInfo(List<string> usersIds, int pageNumber, int pageSize);
    }
}
