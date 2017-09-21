using System.Collections.Generic;
using System.Threading.Tasks;
using ClaBot.Models.Github;
using System.Linq;

namespace ClaBot.Controllers
{
    public class StatusController : GitHubControllerBase
    {
        string Context => "license-agreement/ClaBot";

        /// <summary>
        /// Adds a pending status to the specified pull request
        /// </summary>
        /// <param name="owner">The repository owner</param>
        /// <param name="reponame">The repository name</param>
        /// <param name="id">The SHA of the pull request</param>
        /// <param name="accessToken">The access token for the GitHub API</param>
        /// <returns>The latest status</returns>
        /// <remarks>If the status is already pending for the ClaBot context, then a new status will not be added. Regardless of any
        /// previous error, failure or successful check, the pull request will re-enter a pending status while the system re-verifies 
        /// that all committers have a valid CLA.  This is done to ensure that any new commits on a previously verified request will be
        /// properly included in the verification.</remarks>
        public async Task<Status> SetPendingStatusAsync(string owner, string reponame, string id, AccessToken accessToken)
        {
            List<Status> statuses = await GitHubApi.GetStatuses(owner, reponame, id);

            Status currentStatus = (from s in statuses
                                    where s.Context == Context
                                    select s).FirstOrDefault();



            if (currentStatus?.State != "pending") //statuses.Count == 0 || 
            {
                //TODO: Need to add a TargetURL to the CLA app with the appropriate context for the project
                currentStatus = await SetStatusAsync(owner,reponame,id, StatusState.pending, accessToken);
            }

            return currentStatus;
        }

        public async Task<Status> SetStatusAsync(string owner, string reponame, string id, StatusState state, AccessToken accessToken)
        {
            Status status = new Status
            {
                State = state.ToString(),
                TargetUrl = "https://jbrinkman.github.io/ClaBot",
                Description = "Checking for a valid CLA",
                Context = "license-agreement/ClaBot"
            };

            status = await GitHubApi.SetStatus(owner, reponame, id, $"token {accessToken.Token}", status);

            return status;
        }

        public static StatusController Instance()
        {
            return new StatusController();
        }
    }
}
