using ClaBot.Models.Github;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClaBot.Services.Github
{
    [Headers("User-Agent: ClaBot Function", 
             "Accept: application/vnd.github.machine-man-preview+json")]
    public interface IGitHubApi
    {
        [Get("/repos/{owner}/{repo}/commits/{refId}/statuses")]
        Task<List<Status>> GetStatuses(string owner, string repo, string refId);

        [Post("/repos/{owner}/{repo}/statuses/{refId}")]
        Task<Status> SetStatus(string owner, string repo, string refId, [Header("Authorization")] string authorization, [Body] Status status);

        [Post("/installations/{installationId}/access_tokens")]
        Task<AccessToken> GetAccessToken(string installationId, [Header("Authorization")] string authorization);
    }
}
