namespace DynamicWorkflow.Aggregator.Interface
{
    public interface IWorkflowDataHelper
    {
        public List<UsersDataResponse> GetUsersData(params string[] usersIds);
        public List<OrgDataResponse> GetOrgsData(params string[] orgIds);
        public List<RolesDataResponse> GetRolesData(params string[] roles);
        public List<SlaDataResponse> GetSlaData(params string[] slaList);
        public List<RolesDataResponse> GetRolesByCategoryId(string categoryId);
        public List<string> GetRolesByUserId(string userid);
    }
}
