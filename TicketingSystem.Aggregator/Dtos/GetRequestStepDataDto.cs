namespace DynamicWorkflow.Aggregator.Dtos
{
    public class GetRequestStepDataDto
    {
        public GetRequestStepDataDto()
        {

        }
        public GetRequestStepDataDto(int stepNumber, string slaId, int actionTackerType, string actionTackerId, string roleId)
        {
            StepNumber = stepNumber;
            SlaId = slaId;
            ActionTackerType = (ActionTakerTypes)actionTackerType;
            ActionTakerId = actionTackerId;
            RoleId = roleId;
        }
        public int StepNumber { get; set; }
        public string SlaId { get; set; }
        public int ImplementationDays { get; set; }
        public int ImplementationHours { get; set; }
        public int ImplementationMinutes { get; set; }
        public string ActionTakerId { get; set; }
        public ActionTakerTypes ActionTackerType { get; set; }
        public string RoleId { get; set; }
        public string ActionTackerName { get; set; }
        public string RoleName { get; set; }
    }

}
