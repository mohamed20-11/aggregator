using DynamicWorkflow.Aggregator.IGrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace DynamicWorkflow.Aggregator.Controllers.EscalationLevel
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscalationLevelController : ControllerBase
    {
        private readonly IEscalationLevel _escalationLevel;

        public EscalationLevelController(IEscalationLevel escalationLevel)
        {
            _escalationLevel = escalationLevel;
        }

        [HttpGet]
        public async Task<ResponseDTO> GetAllEscalationLevels(int pageSize, int pageNumber)
        {
            return await _escalationLevel.GetAllEscalationLevels(pageSize, pageNumber);
        }

        [HttpGet("{escalationLevelId}")]
        public async Task<ResponseDTO> GetEscalationLevelById(Guid escalationLevelId)
        {
            return await _escalationLevel.GetEscalationLevelById(escalationLevelId);
        }
    }
}
