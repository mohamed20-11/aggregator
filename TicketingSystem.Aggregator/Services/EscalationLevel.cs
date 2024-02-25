using DynamicWorkflow.Aggregator.IGrpcServices;
using DynamicWorkflow.Workflow.Application.Protos;
using TicketingSystem.Aggregator.Dtos;

namespace DynamicWorkflow.Aggregator.Services
{
    public class EscalationLevel : IEscalationLevel
    {
        private readonly EscalationLevelProtoService.EscalationLevelProtoServiceClient _esLevelClinet;
        private readonly IResponseHelper _responseHelper;
        private readonly UserProtoService.UserProtoServiceClient _userClinet;
        private ResponseDTO _responseDTO;

        public EscalationLevel(EscalationLevelProtoService.EscalationLevelProtoServiceClient esLevelClinet,
                         IResponseHelper responseHelper,
                         UserProtoService.UserProtoServiceClient userClinet)
        {
            _esLevelClinet = esLevelClinet;
            _responseHelper = responseHelper;
            _userClinet = userClinet;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetAllEscalationLevels(int pageSize, int pageNumber)
        {
            //GET All EscalationLevel From Sla
            var allEscalationLevels = _esLevelClinet.GetAllEscalationLevels(new RequestAllEscalationLevels { PageIndex = pageNumber, PageSize = pageSize });

            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(allEscalationLevels?.EscalationLevel?.Select(x => x.UserId));

            //GET All users Data based On list of their ids
            var allUserData = _userClinet.GetListOfUsers(userIds);

            var allEscalationLevelsMapped = allEscalationLevels?.EscalationLevel?.Select(es => new EscalationLevelDto
            {
                Id = Guid.TryParse(es.Id, out Guid parsedEsId) ? parsedEsId : default(Guid),
                UserId = Guid.TryParse(es.UserId, out Guid parsedUserId) ? parsedUserId : default(Guid),
                FirstName = allUserData?.UserFullname?.Where(user => user.UserId.ToLower() == es.UserId.ToLower())?.FirstOrDefault()?.FirstName ?? "",
                SecondName = allUserData?.UserFullname?.Where(user => user.UserId.ToLower() == es.UserId.ToLower())?.FirstOrDefault()?.SecondName ?? "",
                ThirdName = allUserData?.UserFullname?.Where(user => user.UserId.ToLower() == es.UserId.ToLower())?.FirstOrDefault()?.ThirdName ?? "",
                FourthName = allUserData?.UserFullname?.Where(user => user.UserId.ToLower() == es.UserId.ToLower())?.FirstOrDefault()?.FourthName ?? "",
                EscalationLevelName = es.EscalationLevelName ?? "",
                LevelIndex = es.LevelIndex,
                ResponseDays = es.ResponseDays,
                ResponseHours = es.ResponseHours,
                ResponseMinutes = es.ResponseMinutes,
                SLAId = Guid.TryParse(es.SlaId, out Guid parsedSlaId) ? parsedSlaId : default(Guid),

            }).ToList();


            _responseDTO = _responseHelper.RetrievedSuccessfully(allEscalationLevelsMapped, "allEscalationLevelsRetrievedSuccessfully");
            _responseDTO.PageSize = allEscalationLevels.PageSize;
            _responseDTO.PageIndex = allEscalationLevels.PageIndex;
            _responseDTO.TotalPages = allEscalationLevels.TotalPages;
            _responseDTO.TotalItems = allEscalationLevels.TotalItems;

            return _responseDTO;
        }

        public async Task<ResponseDTO> GetEscalationLevelById(Guid escalationLevelId)
        {
            var escalationLevel = _esLevelClinet.GetEscalationLevelById(new RequestEscalationLevelById { EscalationLevelId = escalationLevelId.ToString() });

            if (escalationLevel.EscalationLevel == null)
            {
                return _responseHelper.NotFound("escalationLevelIsNotFound");
            }

            var requestedUserId = new RequestGetUserById();
            requestedUserId.UserId = escalationLevel?.EscalationLevel?.UserId;

            var userData = _userClinet.GetUserById(requestedUserId);

            var escalationLevelObj = new EscalationLevelDto
            {
                Id = Guid.TryParse(escalationLevel?.EscalationLevel?.Id, out Guid parsedEsId) ? parsedEsId : default(Guid),
                UserId = Guid.TryParse(escalationLevel?.EscalationLevel?.UserId, out Guid parsedUserId) ? parsedUserId : default(Guid),
                FirstName = userData.FirstName ?? "",
                SecondName = userData.SecondName ?? "",
                ThirdName = userData.ThirdName ?? "",
                FourthName = userData.FourthName ?? "",
                EscalationLevelName = escalationLevel?.EscalationLevel?.EscalationLevelName ?? "",
                LevelIndex = escalationLevel?.EscalationLevel?.LevelIndex,
                ResponseDays = escalationLevel?.EscalationLevel?.ResponseDays,
                ResponseHours = escalationLevel?.EscalationLevel?.ResponseHours,
                ResponseMinutes = escalationLevel?.EscalationLevel?.ResponseMinutes,
                SLAId = Guid.TryParse(escalationLevel?.EscalationLevel?.SlaId, out Guid parsedSlaId) ? parsedSlaId : default(Guid),
            };

            return _responseHelper.RetrievedSuccessfully(escalationLevelObj, "escalationLevelIsRetrievedSuccessfully");
        }
    }
}
