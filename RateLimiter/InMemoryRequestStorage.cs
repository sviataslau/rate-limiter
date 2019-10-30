using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public class InMemoryRequestStorage : IRequestStorage
	{
		public List<IUserRequest> UserRequests { get; } = new List<IUserRequest>();

		public IEnumerable<DateTime> GetForTimespan(UserToken token, DateTime time)
		{
			return UserRequests.Where(x => x.Time > time).Select(x => x.Time);
		}

		public DateTime? GetLast(UserToken token)
		{
			var result = UserRequests.LastOrDefault();

			return result == null ? (DateTime?)null : result.Time;
		}

		public void Register(UserToken token, IUserRequest request)
		{
			UserRequests.Add(request);
		}
	}
}
