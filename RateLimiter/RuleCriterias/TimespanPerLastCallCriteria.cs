using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.RuleCriterias
{
	class TimespanPerLastCallCriteria : IRuleCriteria
	{
		public bool Check(IUserRequest request, IRequestStorage storage)
		{
			throw new NotImplementedException();
		}
	}
}
