namespace DynamicWorkflow.Aggregator.Helpers.Enums
{
    public enum ActionOptions
    {
        Aprove,
        Regect,
        Edit,
        redirection,
    }
    public enum ActionTakerTypes
    {
        Employee,
        Organization,
        Committee,
        Customized
    }
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        RequireEdit,
        Redirected
    }
    public enum SlaStatus
    {
        Important = 1,
        Urgent = 2,
        Normal = 3,
        Summary = 4
    }
}
