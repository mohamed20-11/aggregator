namespace DynamicWorkflow.Aggregator.Interface
{
    public interface IWorkflowPermission
    {
        bool CanUserTakeAction(RequestTemplateStepResponse step);
    }
}
