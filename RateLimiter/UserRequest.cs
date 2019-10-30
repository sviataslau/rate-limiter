using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public class UserRequest : IUserRequest
	{
		public UserToken Token { get; set; }
		public DateTime Time { get; set; }
	}
}
