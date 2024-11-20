using Newtonsoft.Json;
using ProspectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ProspectManager.Services
{
    public class ApiService(HttpClient httpClient) : IApiService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _url = "http://10.0.2.2:39257";

        public async Task<ProspectModel> GetActivitiesByProspectIdAsync(string prospectId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/Activities?prospectId={prospectId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ProspectModel>() ?? new ProspectModel() { CellPhoneNumber = "", Email = "", Name = "" };
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ProspectModel>> GetProspectsAsync()
        {

            var response = await _httpClient.GetAsync($"{_url}/api/Prospects");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProspectModel>>() ?? [];

        }

        public async Task CreateActivityAsync(ActivityModelInput activity)
        {
            var strings = JsonConvert.SerializeObject(activity);
            var response = await _httpClient.PostAsJsonAsync($"{_url}/api/Activities", activity);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateActivityAsync(ActivityModelInput activity)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_url}/api/Activities", activity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteActivityAsync(int activityId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/api/Activities?id={activityId}");
            response.EnsureSuccessStatusCode();
        }


    }
}
