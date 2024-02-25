using DynamicWorkflow.Aggregator.IGrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace DynamicWorkflow.Aggregator.Controllers.RequestTemplate
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTemplateController : ControllerBase
    {
        private readonly IRequestTemplateService _requestTemplateService;

        public RequestTemplateController(IRequestTemplateService requestTemplateService)
        {
            _requestTemplateService = requestTemplateService;
        }

        [HttpGet]
        public async Task<ResponseDTO> GetRequestTemplateById(Guid id)
        {
            return await _requestTemplateService.GetRequestTemplateById(id);
        }
    }
}
