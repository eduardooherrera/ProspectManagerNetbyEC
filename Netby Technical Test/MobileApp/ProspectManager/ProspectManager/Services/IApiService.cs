using ProspectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectManager.Services
{
    public interface IApiService
    {
        Task<List<ProspectModel>> GetProspectsAsync();
        Task<ProspectModel> GetActivitiesByProspectIdAsync(string prospectId);
        Task CreateActivityAsync(ActivityModelInput activity);
        Task UpdateActivityAsync(ActivityModelInput activity);
        Task DeleteActivityAsync(int activityId);
    }
}
