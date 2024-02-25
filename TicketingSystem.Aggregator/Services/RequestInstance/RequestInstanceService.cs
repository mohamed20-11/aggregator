
namespace DynamicWorkflow.Aggregator.Services.RequestInstance
{
    public class RequestInstanceService : IRequestInstanceService
    {
        private readonly RequestInstanceManager.RequestInstanceManagerClient _requestInstanceClient;
        private readonly ResponseHelper _responseHelper;
        private readonly IWorkflowDataHelper _workFlowDataHelper;
        private readonly List<ActionTackerDataRequest> _userDataRequest;

        public RequestInstanceService(RequestInstanceManager.RequestInstanceManagerClient requestInstanceClient, IWorkflowDataHelper workFlowDataHelper)
        {
            _requestInstanceClient = requestInstanceClient;
            _responseHelper = new ResponseHelper();
            _workFlowDataHelper = workFlowDataHelper;
            _userDataRequest = new List<ActionTackerDataRequest> { };
        }

        public async Task<ResponseDTO> GetRequestInstanceById(Guid requestInstanceId)
        {
            var requestInstanceIdRequest = new RequestInstanceId
            {
                Id = requestInstanceId.ToString(),
            };

            var requestInstance = _requestInstanceClient.GetRequestInstanceById(requestInstanceIdRequest);

            var usersIds = requestInstance.RequestTransactions.Select(x => new ActionTackerDataRequest
            {
                ActionTackerId = x.UserId,
                ActionTackerType = ActionTakerTypes.Employee
            }).ToList();

            _userDataRequest.AddRange(usersIds);
            _userDataRequest.Add(new ActionTackerDataRequest
            {
                ActionTackerId = requestInstance.CreatedBy,
                ActionTackerType = ActionTakerTypes.Employee
            });

            var usersData = _workFlowDataHelper.GetUsersData(_userDataRequest.Select(a => a.ActionTackerId).ToArray());

            var requestInstanceResponse = new RequestInstanceDto()
            {
                Name = requestInstance.Name,
                Description = requestInstance.Description,
                RequesterName = usersData?.Where(x => string.Compare(x.UserId, requestInstance.CreatedBy, true) == 0)
                .Select(x => x.UserName)
                .FirstOrDefault(),
                RequestDate = DateTime.Parse(requestInstance.CreatedOn),
            };

            requestInstanceResponse.Transactions.AddRange(requestInstance.RequestTransactions.Select(x => new RequestTransactionDto
            {
                Comment = x.Comment,
                UserName = usersData?.Where(a => string.Compare(a.UserId, x.UserId, true) == 0)
                .Select(x => x.UserName)
                .FirstOrDefault(),
                UserImage = usersData?.Where(a => string.Compare(a.UserId, x.UserId, true) == 0)
                .Select(x => x.ImageUrl)
                .FirstOrDefault(),
                ReceivedDate = DateTime.Parse(x.DecisionDate),
                AcceptanceDate = DateTime.Parse(x.CreatedOn)
            }));

            return _responseHelper.RetrievedSuccessfully(requestInstanceResponse, "Success");
        }
    }
}
