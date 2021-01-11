using Mythical.RenderModels;
using MythicalWebApp.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MythicalWebApp.Data
{
    public class ServicesService
    {
        public Task<List<ServiceRender>> GetServices()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:44304/api/main").Result;
            string callResult = "";
            List<ServiceRender> serviceRenders = new List<ServiceRender>();
            if (response.IsSuccessStatusCode)
            {
                callResult = response.Content.ReadAsStringAsync().Result;
                serviceRenders = JsonConvert.DeserializeObject<List<ServiceRender>>(callResult);
            }

            return Task.FromResult(serviceRenders);
        }

        public Task<ServiceRender> GetService(string id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44304/api/main/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            string callResult = "";
            ServiceRender serviceRender = null;
            if (response.IsSuccessStatusCode)
            {
                callResult = response.Content.ReadAsStringAsync().Result;
                serviceRender = JsonConvert.DeserializeObject<ServiceRender>(callResult);
            }

            return Task.FromResult(serviceRender);
        }
    }
}
