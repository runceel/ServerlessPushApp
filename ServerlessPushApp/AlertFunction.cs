using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessPushApp
{
    public class AlertFunction
    {
        [FunctionName("Alert")]
        public static async Task Alert(
            [CosmosDBTrigger("serverlesspushdb", 
                "data", 
                ConnectionStringSetting = "DefaultCosmosDB", 
                CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents,
            [SignalR(HubName = "alert")] IAsyncCollector<SignalRMessage> messages,
            ILogger log)
        {
            log.LogInformation($"{documents.Count} items updated. The first data's id is {documents.First().Id}.");
            var alertTargets = documents.Select(x => (Data)(dynamic)x)
                .Where(x => x.Amount >= 100)
                .ToArray();
            if (alertTargets.Any())
            {
                log.LogInformation($"{alertTargets.Length} items are outlier.");
                await messages.AddAsync(new SignalRMessage
                {
                    Target = "alert",
                    Arguments = new object[] { alertTargets },
                });
            }
        }
    }
}
