using Microsoft.AspNetCore.Mvc;

namespace DynamicWorkflow.Aggregator.Controllers.RequestInstance
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestInstanceController : ControllerBase
    {
        private IRequestInstanceService _requestInstanceService;

        public RequestInstanceController(IRequestInstanceService requestInstanceService)
        {
            _requestInstanceService = requestInstanceService;
        }

        [HttpGet]
        public async Task<ResponseDTO> GetRequestInstanceById(Guid id)
        {
            return await _requestInstanceService.GetRequestInstanceById(id);
        }
    }
}
