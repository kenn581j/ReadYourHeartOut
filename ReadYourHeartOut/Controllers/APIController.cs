using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Controllers
{
    public static class APIController
    {
        public static HttpClient apiClient { get; set;  }


        public static void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("https://localhost:");
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        }
    }
}
