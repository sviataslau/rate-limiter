using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class RequestLimitChecker
    {
        private Dictionary<Issuer, List<Request>> IsserRequests { get; set; }

        public RequestLimitChecker(List<Issuer> issuers)
        {
            IsserRequests = new Dictionary<Issuer, List<Request>>();
            issuers.ForEach(x => IsserRequests.Add(x, null));
        }

        public LimitCheckerResponse CheckAndFire(string accessToken,
                                                 RequestType request,
                                                 DateTimeOffset currentDate,
                                                 Action successAction)
        {
            var currentIssuer = IsserRequests.FirstOrDefault(x => x.Key.AccessToken == accessToken);
            var issuerCurrentTypeOfRequest = currentIssuer.Value?.FirstOrDefault(x => x.RequestType == request);

            if (currentIssuer.Value == null)
            {
                IsserRequests[currentIssuer.Key] = new List<Request>()
                {
                    new Request
                    {
                        RequestType =  request,
                        LastRequestDate =  currentDate
                    }
                };


            }
            else if (issuerCurrentTypeOfRequest != null &&
                     issuerCurrentTypeOfRequest.LastRequestDate.AddMilliseconds(currentIssuer.Key
                         .DelaysInMilliseconds) <= currentDate)
            {
                issuerCurrentTypeOfRequest.LastRequestDate = currentDate;
            }
            else if (issuerCurrentTypeOfRequest == null)
            {
                currentIssuer.Value.Add(new Request()
                {
                    RequestType = request,
                    LastRequestDate = currentDate
                });
            }
            else if (issuerCurrentTypeOfRequest.LastRequestDate.AddMilliseconds(currentIssuer.Key
                         .DelaysInMilliseconds) > currentDate)
            {
                return LimitCheckerResponse.NotPassed;
            }
            successAction?.Invoke();
            return LimitCheckerResponse.Passed;
        }

    }

}

public class Issuer
{
    public string AccessToken { get; set; }
    public int DelaysInMilliseconds { get; set; }
}

public class Request
{
    public RequestType RequestType { get; set; }
    public DateTimeOffset LastRequestDate { get; set; }
}

public enum RequestType
{
    A,
    B,
    C
}

public enum LimitCheckerResponse
{
    Passed,
    NotPassed
}


