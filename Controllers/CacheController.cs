using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaBot.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace ClaBot.Controllers
{
    public class CacheController: DBControllerBase
    {
        protected override string Collection => "cache";

        public async Task AddCacheItemAsync(string key, object doc)
        {
             await Client.UpsertDocumentAsync(
                 CollectionUri, 
                 new CacheItem {
                    Key = key,
                    Item = doc,
                    TimeToLive = 60 * 9
                 }
             );
        }

        public object GetCacheItem(string key)
        {
            CacheItem ci = Client.CreateDocumentQuery<CacheItem>(CollectionUri)
                .Where(f => f.Key == key)
                .AsEnumerable<CacheItem>()
                .FirstOrDefault();

            return ci?.Item;
        }

        public static CacheController Instance() {
            return new CacheController();
        }
    }
}
