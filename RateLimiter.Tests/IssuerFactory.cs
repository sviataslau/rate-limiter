using System;
using System.Collections.Generic;

namespace RateLimiter.Tests
{
    public class IssuerFactory
    {
        public static List<Issuer> GenerateIssuers()
        {
            return new List<Issuer>()
            {
                new Issuer()
                {
                    DelaysInMilliseconds = 5*1000,
                    AccessToken =  Guid.NewGuid().ToString()
                },
                new Issuer()
                {
                    DelaysInMilliseconds = 8*1000,
                    AccessToken =  Guid.NewGuid().ToString()
                },
                new Issuer()
                {
                    DelaysInMilliseconds = 3*1000,
                    AccessToken =  Guid.NewGuid().ToString()
                },
            };
        }
    }
}
