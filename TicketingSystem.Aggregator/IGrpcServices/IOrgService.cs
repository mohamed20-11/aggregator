using ControlPanel.ORGHierararchy.Protos;

namespace DynamicWorkflow.Aggregator.IGrpcServices
{
    public interface IOrgService
    {
        Task<GetOrgHierarchyByIdsResponse> GetOrgsByIds(List<Guid> orgIdsList);
    }
}
