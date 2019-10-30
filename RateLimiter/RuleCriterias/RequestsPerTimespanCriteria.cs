using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.RuleCriterias
{
	public class RequestsPerTimespanCriteria : IRuleCriteria
	{
		private const int LimitSeconds = 10;
		private const int RequestPerLimit = 2;

		public bool Check(IUserRequest request, IRequestStorage storage)
		{
			var start = request.Time - TimeSpan.FromSeconds(LimitSeconds);
			var lastRequests = storage.GetForTimespan(request.Token, start);

			return lastRequests.Count() <= RequestPerLimit;
		}
	}
}
