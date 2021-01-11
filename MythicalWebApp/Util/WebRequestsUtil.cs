using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MythicalWebApp.Util
{
    public static class WebRequestsUtil
    {
        static HttpClient client = new HttpClient();

        public static string CallApi(string url, string ApiKey = "", Dictionary<string, string> RequestParams = null)
        {
            HttpClient client = new HttpClient();

            if (RequestParams != null)
                url = QueryHelpers.AddQueryString(url, RequestParams);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //request.Headers.Add("keycode", "");

            var requestResponse = client.SendAsync(request).Result;

            if (requestResponse.IsSuccessStatusCode)
                return requestResponse.Content.ReadAsStringAsync().Result;
            else
                throw new Exception($"API call failed : {url} returned {requestResponse.StatusCode.ToString()}");
        }
    }
}
