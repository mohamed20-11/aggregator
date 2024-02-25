namespace DynamicWorkflow.Aggregator.Interface
{
	public interface ITokenService
	{
		Task<CallOptions> AcquireToken();
		Task<CallOptions> GetAccessTokenAsync();
	}
}
