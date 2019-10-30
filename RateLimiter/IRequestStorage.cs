using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public interface IRequestStorage
	{
		void Register(UserToken token, IUserRequest request);
		DateTime? GetLast(UserToken token);
		IEnumerable<DateTime> GetForTimespan(UserToken token, DateTime fromTime);
	}
}
