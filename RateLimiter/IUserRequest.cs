using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
	public interface IUserRequest
	{
		UserToken Token { get; set; }
		DateTime Time { get; set; }
	}
}
