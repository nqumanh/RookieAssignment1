using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
namespace CustomerSite.Helper
{
    public class APIHelper
    {
        public HttpClient initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7133/");
            return client;
        }
    }
}