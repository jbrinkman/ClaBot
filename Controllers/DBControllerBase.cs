using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace ClaBot.Controllers
{
    public abstract class DBControllerBase
    {
        protected Uri DbUri => new Uri(SettingsController.Instance().EndpointUrl);
        protected DocumentClient Client => new DocumentClient(DbUri, SettingsController.Instance().PrimaryKey);

        protected string DbName => "clabot";
        protected Uri CollectionUri => UriFactory.CreateDocumentCollectionUri(DbName, Collection);
        protected abstract string Collection
        {
            get;
        }

        public async void Init() {
            await Client.CreateDatabaseIfNotExistsAsync(new Database { Id = DbName });

            await Client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(DbName), 
                new DocumentCollection { Id = Collection, DefaultTimeToLive = -1 },
                new RequestOptions { OfferThroughput = 400,  });
        }

    }
}
