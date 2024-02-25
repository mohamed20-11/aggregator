
namespace DynamicWorkflow.Aggregator.Helpers.WorkFlow
{
    public class WorkFlowDataHelper : IWorkflowDataHelper
    {
        private readonly OrgProtoService.OrgProtoServiceClient _orgClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly RoleWorkspaceProtoService.RoleWorkspaceProtoServiceClient _roleClient;
        private readonly SlaProtoServices.SlaProtoServicesClient _slaProtoServicesClient;

        public WorkFlowDataHelper(OrgProtoService.OrgProtoServiceClient orgClient
            , UserProtoService.UserProtoServiceClient userClient,
            RoleWorkspaceProtoService.RoleWorkspaceProtoServiceClient roleClient,
            SlaProtoServices.SlaProtoServicesClient slaProtoServicesClient)
        {
            _orgClient = orgClient;
            _userClient = userClient;
            _roleClient = roleClient;
            _slaProtoServicesClient = slaProtoServicesClient;
        }


        public List<SlaDataResponse> GetSlaData(params string[] slaList)
        {
            if (slaList.Length == 0)
                return new List<SlaDataResponse>();

            var sladataRequest = new SlaIds();
            sladataRequest.SlaIds_.AddRange(slaList);
            var slaListRsponse = _slaProtoServicesClient.GetSlaDataList(sladataRequest);
            if (slaListRsponse is not null && slaListRsponse.SlaList.Count > 0)
            {
                return slaListRsponse.SlaList.Select(a => new SlaDataResponse()
                {
                    SlaId = a.SlaId,
                    SlaName = a.SlaName,
                    ImplementDays = a.ImplementDays,
                    ImplementHours = a.ImplementHours,
                    ImplementMinutes = a.ImplementMinutes,

                }).ToList();

            }
            return new List<SlaDataResponse>();

        }

        public List<RolesDataResponse> GetRolesData(params string[] roles)
        {
            if (roles.Length == 0)
                return new List<RolesDataResponse>();

            var rolesRequest = new RolesIdsRequest();
            rolesRequest.RolesId.AddRange(roles);
            var rolesData = _roleClient.GetRolesByIds(rolesRequest);
            if (rolesData is not null && rolesData.RolesList.Count > 0)
            {
                return rolesData.RolesList.Select(r => new RolesDataResponse
                {
                    RoleId = r.RoleId,
                    RoleNameAr = r.RoleNameAr,

                }).ToList();
            }
            return new List<RolesDataResponse>();
        }


        public List<UsersDataResponse> GetUsersData(params string[] usersIds)
        {
            var usersIdsRequest = new GetAllUsersByIdsRequest();
            if (!usersIds.Any())
                return new List<UsersDataResponse>();
            usersIdsRequest.UserIds.AddRange(usersIds);
            var usersData = _userClient.GetAllUsersByIds(usersIdsRequest);
            if (usersData is not null && usersData.Users.Count > 0)
            {
                return usersData.Users.Select(u => new UsersDataResponse
                {
                    UserId = u.UserId,
                    UserName = string.Concat(u.FirstName ?? "", " ", u.SecondName ?? "").Trim(),
                    ImageUrl = u.ImageUrl,
                }).ToList();
            }
            return new List<UsersDataResponse>();

        }

        public List<OrgDataResponse> GetOrgsData(params string[] OrgIds)
        {
            if (!OrgIds.Any())
                return new List<OrgDataResponse>();

            var orgIdsRequest = new GetOrgHierarchysByIdsRequest();
            orgIdsRequest.OrgId.AddRange(OrgIds);
            var orgData = _orgClient.GetOrgHierarchysByIds(orgIdsRequest);

            if (orgData is not null && orgData.Orgs.Count > 0)
            {
                return orgData.Orgs.Select(org => new OrgDataResponse
                {
                    OrgId = org.Id,
                    OrgNameAr = org.NameAr,
                    OrgNameEn = org.NameEn,
                }).ToList();
            }
            return new List<OrgDataResponse>();
        }

        public List<RolesDataResponse> GetRolesByCategoryId(string categoryId)
        {
            var rolesResponse = new List<RolesDataResponse>();
            var roles = _roleClient.GetRolesByCategoryId(new RoleCategoryIdRequest { CategoryId = categoryId });
            if (roles is not null && roles.RolesList.Any())
                rolesResponse.AddRange(roles.RolesList.Select(r => new RolesDataResponse
                {
                    RoleId = r.RoleId,
                    RoleNameAr = r.RoleNameAr,
                    RoleNameEn = r.RoleNameEn,
                }));

            return rolesResponse;

        }

        public List<string> GetRolesByUserId(string userid)
        {
            var rolesResponse = new List<string>();
            var userRolesResponse = _userClient.GetUserRoles(new RequestUserId { UserId = userid });
            if (userRolesResponse is not null && userRolesResponse.RoleId.Count > 0)
                rolesResponse.AddRange(userRolesResponse.RoleId);

            return rolesResponse;
        }
    }
}
