using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace MyNursingFuture.Api.Infrastructure
{
    public static class HttpClientManager
    {
        private static HttpClient _client;
        
        public static HttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = new HttpClient();
                }
                return _client;
            }
        }
    }
}