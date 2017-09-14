using ClaBot.Services.Github;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClaBot.Models.Github;

namespace ClaBot.Controllers
{
    public static class StatusController
    {
        public static async Task<List<Status>> SetPendingStatusAsync(string owner, string reponame, string id, AccessToken accessToken)
        {
            IGitHubApi gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
            List<Status> statuses = await gitHubApi.GetStatuses(owner, reponame, id);


            if (statuses.Count == 0)
            {
                Status status = new Status
                {
                    State = "pending",
                    TargetUrl = "https://jbrinkman.github.io/ClaBot",
                    Description = "Checking for a valid CLA",
                    Context = "license-agreement/ClaBot"

                };
                status = await gitHubApi.SetStatus(owner, reponame, id, $"token {accessToken.Token}", status);
                statuses.Add(status);
            }

            return statuses;
        }
    }
}
