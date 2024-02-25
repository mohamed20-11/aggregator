using DynamicWorkflow.Aggregator.Dtos;

namespace DynamicWorkflow.Aggregator.Helpers.WorkFlow
{
    public class RquestTemplatehelper : IRquestTemplatehelper
    {
        private readonly IWorkflowDataHelper _workflowDataHelper;
        private List<GetRequestStepDataDto> _stepsList = new List<GetRequestStepDataDto>();

        public RquestTemplatehelper(
            IWorkflowDataHelper workflowDataHelper)
        {

            _workflowDataHelper = workflowDataHelper;
        }

        private void AddEmployeeData(string[] usersList)
        {

            var usersData = _workflowDataHelper.GetUsersData(usersList.ToArray());
            if (usersData is not null && usersData.Count > 0)
            {
                foreach (var step in _stepsList)
                {
                    if (step.ActionTackerType == ActionTakerTypes.Employee)
                        step.ActionTackerName = usersData.Where(u => string.Compare(u?.UserId, step?.ActionTakerId, true) == 0)
                                                             .Select(u => u.UserName)
                                                             .FirstOrDefault() ?? "";
                }

            }
        }

        private void AddOrgData(string[] orglist)
        {


            var orgData = _workflowDataHelper.GetOrgsData(orglist);

            if (orgData is not null && orgData.Count > 0)
            {
                foreach (var step in _stepsList)
                {
                    if (step.ActionTackerType == ActionTakerTypes.Organization)
                    {
                        step.ActionTackerName = orgData.Where(o => string.Compare(o.OrgId, step.ActionTakerId, true) == 0)
                                                          .Select(o => o.OrgNameAr)
                                                          .FirstOrDefault() ?? "";
                    }
                }
            }
        }
        private void AddRolesData(string[] rolesList)
        {

            var rolesData = _workflowDataHelper.GetRolesData(rolesList.ToArray());
            if (rolesData is not null && rolesData.Count > 0)
            {
                foreach (var step in _stepsList)
                {
                    step.RoleName = rolesData.Where(r => string.Compare(r.RoleId, step.RoleId, true) == 0)
                                                       .Select(r => r.RoleNameAr)
                                                       .FirstOrDefault() ?? "";
                }
            }

        }
        private void AddSlaData(string[] slaList)
        {
            var slaData = _workflowDataHelper.GetSlaData(slaList);

            if (slaData is not null && slaData.Count > 0)
            {
                foreach (var item in _stepsList)
                {
                    item.ImplementationDays = slaData.Where(s => s.SlaId == item.SlaId).Select(s => s.ImplementDays).FirstOrDefault();
                    item.ImplementationHours = slaData.Where(s => s.SlaId == item.SlaId).Select(s => s.ImplementHours).FirstOrDefault();
                    item.ImplementationMinutes = slaData.Where(s => s.SlaId == item.SlaId).Select(s => s.ImplementMinutes).FirstOrDefault();

                }
            }
        }

        public List<GetRequestStepDataDto> GetStepsData(List<GetRequestStepDataDto> requestSteps)
        {
            _stepsList = requestSteps;
            if (requestSteps.Count > 0)
            {
                var slaList = _stepsList.Select(s => s.SlaId).ToArray();
                AddSlaData(slaList);

                var orgsList = _stepsList.Where(s => s.ActionTackerType == ActionTakerTypes.Organization)
                                           .Select(s => s.ActionTakerId).ToArray();
                if (orgsList.Length > 0)
                    AddOrgData(orgsList);

                var usersList = _stepsList.Where(s => s.ActionTackerType == ActionTakerTypes.Employee)
                                           .Select(s => s.ActionTakerId)
                                           .ToArray();
                if (usersList.Length > 0)
                    AddEmployeeData(usersList);

                var rolesIds = _stepsList.Select(s => s.RoleId).ToArray();
                AddRolesData(rolesIds);
            }
            return _stepsList;

        }


    }
}
