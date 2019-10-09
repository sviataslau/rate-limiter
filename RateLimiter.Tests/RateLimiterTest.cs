using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RateLimiter.Tests
{
    [TestFixture]
    public class RateLimiterTest
    {
        [Test]
        public void Returns_NotPassed_On_The_Same_Type_Of_Request()
        {
            var issuers = IssuerFactory.GenerateIssuers();
            var requestStore = new RequestLimitChecker(issuers);
            var currentDate = DateTimeOffset.Now;

            var responseOnFirstRequest = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(responseOnFirstRequest, LimitCheckerResponse.Passed);

            var responseOnSecondRequest = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(responseOnSecondRequest, LimitCheckerResponse.NotPassed);
        }

        [Test]
        public void Success_On_Same_Type_Of_Request()
        {
            var issuers = IssuerFactory.GenerateIssuers();
            var requestStore = new RequestLimitChecker(issuers);
            var currentDate = DateTimeOffset.Now;
            var calledFirstTime = false;
            var calledSecondTime = false;

            requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, () =>
             {
                 //api request ....
                 calledFirstTime = true;
             });
            Assert.IsTrue(calledFirstTime);

            requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.B, currentDate, () =>
            {
                //api request ....
                calledSecondTime = true;
            });
            Assert.IsTrue(calledSecondTime);
        }

        [Test]
        public void Success_With_Same_Type_Two_Times()
        {
            var issuers = IssuerFactory.GenerateIssuers();

            var requestStore = new RequestLimitChecker(issuers);
            var currentDate = DateTimeOffset.Now;

            var firstAnswer = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(firstAnswer, LimitCheckerResponse.Passed);

            var secondRequestDate = currentDate.AddMilliseconds(issuers[0].DelaysInMilliseconds + 10);
            var secondAnswer = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, secondRequestDate, null);


            Assert.AreEqual(secondAnswer, LimitCheckerResponse.Passed);

        }

        [Test]
        public void Success_With_Different_Issuers()
        {
            var issuers = IssuerFactory.GenerateIssuers();

            var requestStore = new RequestLimitChecker(issuers);
            var currentDate = DateTimeOffset.Now;

            var firstAnswer = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(firstAnswer, LimitCheckerResponse.Passed);

            var secondAnswer = requestStore.CheckAndFire(issuers[1].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(secondAnswer, LimitCheckerResponse.Passed);

        }

        [Test]
        public void Fail_With_Same_Issuer()
        {
            var issuers = IssuerFactory.GenerateIssuers();

            var requestStore = new RequestLimitChecker(issuers);
            var currentDate = DateTimeOffset.Now;

            var firstAnswer = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate, null);
            Assert.AreEqual(firstAnswer, LimitCheckerResponse.Passed);

            var secondAnswer = requestStore.CheckAndFire(issuers[0].AccessToken, RequestType.A, currentDate.AddMilliseconds(issuers[0].DelaysInMilliseconds-1), null);
            Assert.AreEqual(secondAnswer, LimitCheckerResponse.NotPassed);

        }
    }
}
