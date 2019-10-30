using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiter.RuleCriterias;

namespace RateLimiter
{
	public class RequestLimiter
	{
		private ICollection<IRuleCriteria> _criterias;
		private IRequestStorage _storage;
		private bool _skipOnError = true; // TODO: move to config
		private ILogger _logger;

		public RequestLimiter(ICollection<IRuleCriteria> criterias, IRequestStorage storage, ILogger logger)
		{
			_criterias = criterias;
			_storage = storage;
			_logger = logger;
		}

		public bool CheckRequest(IUserRequest request)
		{
			foreach (var ruleCriteria in _criterias)
			{
				bool criterisResult = ruleCriteria.Check(request, _storage);

				if (!criterisResult)
				{
					_logger.Log("Error message");

					return false;
				}
				
			}
			_storage.Register(request.Token, request);

			return true;
		}
	}
}
