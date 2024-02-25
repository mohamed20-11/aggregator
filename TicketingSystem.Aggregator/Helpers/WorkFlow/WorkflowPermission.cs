namespace DynamicWorkflow.Aggregator.Helpers
{
    public class WorkflowPermission : IWorkflowPermission
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OrgMembersProtoService.OrgMembersProtoServiceClient _orgMembersClient;
        private readonly IWorkflowDataHelper _workflowDataHelper;

        public WorkflowPermission(IHttpContextAccessor httpContextAccessor,
                                  OrgMembersProtoService.OrgMembersProtoServiceClient orgMembersClient,
                                  IWorkflowDataHelper workflowDataHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _orgMembersClient = orgMembersClient;
            _workflowDataHelper = workflowDataHelper;
        }

        public bool CanUserTakeAction(RequestTemplateStepResponse step)
        {
            var loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor).ToString();

            switch ((ActionTakerTypes)step.ActionTackerType)
            {
                case ActionTakerTypes.Organization:
                    return CanUserTakeOrganizationAction(loggedInUserId, step);
                case ActionTakerTypes.Employee:
                    return step.ActionTakerId == loggedInUserId;
                default:
                    return false;
            }
        }

        private bool CanUserTakeOrganizationAction(string loggedInUserId, RequestTemplateStepResponse step)
        {
            if (!IsUserValid(loggedInUserId, step))
                return false;

            var usersRoles = _workflowDataHelper.GetRolesByUserId(loggedInUserId);
            return usersRoles.Count > 0 && usersRoles.Contains(step.RoleId);
        }

        private bool IsUserValid(string loggedInUserId, RequestTemplateStepResponse step)
        {
            var userOrgRequest = new RequestGetMember
            {
                MemberId = loggedInUserId,
                OrgId = step.ActionTakerId,
            };

            var userMemberResponse = _orgMembersClient.GetMember(userOrgRequest);
            return userMemberResponse != null &&
                   userMemberResponse.MebmerId == loggedInUserId &&
                   userMemberResponse.OrgId == step.ActionTakerId;
        }
    }
}