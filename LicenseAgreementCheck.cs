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

namespace ClaBot
{
    public static class LicenseAgreementCheck
    {
        static string EndpointUrl = ConfigurationManager.AppSettings["Endpoint"]; //"https://clabot.documents.azure.com:443/";
        static string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"]; //"YGtMB7XCrX5LLRBaOlQwRSsUeF50FivoKkm1b6DIz1vI9hRfg5XfjEa0g7yoWKACM1Cuv8qWoA9KVEVs9ITGvQ==";

        [FunctionName("LicenseAgreementCheck")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(WebHookType = "github")]HttpRequestMessage req, TraceWriter log)
        {

            // Get request body
            string json = await req.Content.ReadAsStringAsync();
            GithubEvent githubEvent = json.FromJson<GithubEvent>();

            using (DocumentClient client = new DocumentClient(new System.Uri(EndpointUrl), PrimaryKey)){
                Document logDocument = await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri("clabot", "infolog"), githubEvent);
            }

            var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
            var statuses = await gitHubApi.GetStatuses(githubEvent.Repository.Owner, githubEvent.Repository.Name, githubEvent.PullRequest.Head.Sha);

            return req.CreateResponse(HttpStatusCode.OK, $"From Github: {githubEvent?.PullRequest?.Title}");
        }
    }
}
