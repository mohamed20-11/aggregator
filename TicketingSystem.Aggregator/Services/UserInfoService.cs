using DynamicWorkflow.Aggregator.IGrpcServices;
using DynamicWorkflow.Workflow.Application.Protos;

namespace DynamicWorkflow.Aggregator.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly UserOrgProtoService.UserOrgProtoServiceClient _userClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDto;
        public UserInfoService(UserOrgProtoService.UserOrgProtoServiceClient userClient,
            IResponseHelper responseHelper)

        {
            _userClient = userClient;
            _responseHelper = responseHelper;
            _responseDto = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetUserInfo(List<string> usersIds, int pageNumber, int pageSize)
        {
            try
            {
                var request = new RequestUserByIdsPaginate();
                request.UserId.AddRange(usersIds.Select(a => a.ToString()));
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;

                var response = await _userClient.GetUsersByIdsPaginateAsync(request);
                if (response.ResponsUser.Any())
                {
                    _responseDto.PageSize = response.ResponsDto.PageSize;
                    _responseDto.PageIndex = response.ResponsDto.PageIndex;
                    return _responseHelper.RetrievedSuccessfully(response.ResponsUser, " Users Retrieved successfully", ref _responseDto);
                }

                return _responseHelper.NotFound("the Users Not Found");
            }
            catch (Exception)
            {
                return _responseHelper.Exception();
            }

        }
    }
}

