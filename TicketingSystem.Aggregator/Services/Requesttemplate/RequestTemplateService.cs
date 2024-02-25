namespace DynamicWorkflow.Aggregator.Services.Requesttemplate
{
    public class RequestTemplateService : IRequestTemplateService
    {
        private readonly RequestTemplateProtoService.RequestTemplateProtoServiceClient _requestTemplateServiceClient;
        private readonly IResponseHelper _responseHelper;
        private readonly IRquestTemplatehelper _requestTemplateHelper;

        public RequestTemplateService(
            IResponseHelper responseHelper,
            RequestTemplateProtoService.RequestTemplateProtoServiceClient requestTemplateServiceClient,
            IRquestTemplatehelper rquestTemplatehelper)
        {
            _responseHelper = responseHelper;
            _requestTemplateServiceClient = requestTemplateServiceClient;
            _requestTemplateHelper = rquestTemplatehelper;
        }

        public async Task<ResponseDTO> GetRequestTemplateById(Guid RequestTemplateId)
        {

            var requestTemplateResponse = _requestTemplateServiceClient.GetRequestTemplateById(new RequestTemplateId { Id = RequestTemplateId.ToString() });

            if (requestTemplateResponse.ErrorMessage.IsNullOrEmpty())
            {
                var requestTempalteDto = new GetRequestTemplateByIdDto();

                requestTempalteDto.Name = requestTemplateResponse.Name;
                requestTempalteDto.PathCount = requestTemplateResponse.PathCount;
                requestTempalteDto.CategoryName = requestTemplateResponse.CategoryName;

                var data = _requestTemplateHelper.GetStepsData(requestTemplateResponse.Steps.Select(s => new GetRequestStepDataDto
                (s.StepNumber,
                s.SlaId,
                s.ActionTackerType,
                s.ActionTakerId,
                s.RoleId)).ToList());

                requestTempalteDto.requestTemplateStepDtos.AddRange(data);

                return _responseHelper.RetrievedSuccessfully(requestTempalteDto, "success");
            }
            return _responseHelper.NotFound("Not Found Data");

        }

    }
}

