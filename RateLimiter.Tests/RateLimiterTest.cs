using System;
using System.Collections.Generic;
using NUnit.Framework;
using RateLimiter.RuleCriterias;

namespace RateLimiter.Tests
{
    [TestFixture]
    public class RateLimiterTest
    {



		[SetUp]
	    public void Setup()
	    {

	    }

        [Test]
        public void Example()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void CheckRequest_CriteriasCheckedNotSuccess()
        {
			// arrange
			UserRequest userRequest = new UserRequest()
	        {
		        Time = DateTime.Now.AddSeconds(-8),
		        Token = new UserToken()
	        };
			var storage = new InMemoryRequestStorage();
			var logger = new Logger();
			var criretias = new List<IRuleCriteria>();
			criretias.Add(new RequestsPerTimespanCriteria());

			var target = new RequestLimiter(criretias, storage, logger);

			// act
			var result = target.CheckRequest(userRequest);
			Assert.IsTrue(result);

			result = target.CheckRequest(userRequest);
			Assert.IsTrue(result);

			target.CheckRequest(userRequest);
			result =  target.CheckRequest(userRequest);


			// assert
			Assert.IsFalse(result);

        }

	}
}
