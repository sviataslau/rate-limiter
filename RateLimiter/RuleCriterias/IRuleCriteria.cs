namespace RateLimiter.RuleCriterias
{
	public interface IRuleCriteria
	{
		bool Check(IUserRequest request, IRequestStorage storage);
	}
}
