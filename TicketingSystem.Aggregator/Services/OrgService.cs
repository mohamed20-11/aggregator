
using ControlPanel.ORGHierararchy.Protos;

namespace DynamicWorkflow.Aggregator.Services
{
    public class OrgService : IOrgService
    {
        private readonly OrgProtoService.OrgProtoServiceClient _orgClient;

        public OrgService(OrgProtoService.OrgProtoServiceClient orgClient)
        {
            _orgClient = orgClient;
        }

        public Task<GetOrgHierarchyByIdsResponse> GetOrgsByIds(List<Guid> orgIdsList)
        {
            var orgListRequest = new GetOrgHierarchysByIdsRequest();
            orgListRequest.OrgId.AddRange(orgIdsList.Select(a => a.ToString()));
            var orgDatata = _orgClient.GetOrgHierarchysByIds(orgListRequest);
            return Task.FromResult(orgDatata);
        }
    }
}
