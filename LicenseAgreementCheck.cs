using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using ClaBot.Models.Github;
using Refit;
using ClaBot.Services.Github;
using ClaBot.Controllers;
using System.Collections.Generic;
using ClaBot.Models;

namespace ClaBot
{
    public static class LicenseAgreementCheck
    {


        [FunctionName("LicenseAgreementCheck")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(WebHookType = "github")]HttpRequestMessage req, TraceWriter log)
        {

            // Get request body
            string json = await req.Content.ReadAsStringAsync();
            GithubEvent githubEvent = json.FromJson<GithubEvent>();

            await LogController.Instance().AddLogAsync(LogType.WebHook, githubEvent);

            AccessToken token = await AuthController.Instance().GetAccessToken(githubEvent.Repository.Owner, githubEvent.Repository.Name, githubEvent.Installation.Id);

            List<Status> stats = await StatusController.SetPendingStatusAsync(githubEvent.Repository.Owner, githubEvent.Repository.Name, githubEvent.PullRequest.Head.Sha, token);

            await LogController.Instance().AddLogAsync(LogType.Status, stats);

            return req.CreateResponse(HttpStatusCode.OK, $"From Github: {githubEvent?.PullRequest?.Title}");
        }
    }
}
