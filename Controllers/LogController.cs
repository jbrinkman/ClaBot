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
    public class LogController: DBControllerBase
    {
        protected override string Collection => "infolog";

        public async Task<Document> AddLogAsync(LogType logType, object doc) {

            var logItem = new
            {
                type = logType.ToString(),
                details = doc
            };

            Document logDocument = await Client.UpsertDocumentAsync(CollectionUri, logItem);

            return logDocument;
        }

        public static LogController Instance() {
            return new LogController();
        }
    }
}
