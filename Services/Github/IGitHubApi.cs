using ClaBot.Models.Github;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClaBot.Services.Github
{
    [Headers("User-Agent: ClaBot Function")]
    public interface IGitHubApi
    {
            [Get("/repos/{owner}/{repo}/commits/{refId}/statuses")]
            Task<List<Status>> GetStatuses(string owner, string repo, string refId);
    }
}
