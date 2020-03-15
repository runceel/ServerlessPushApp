using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ServerlessPushApp
{
    public class ImportDataFunction
    {
        [FunctionName("ImportData")]
        public static async Task<IActionResult> ImportData(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "data")] HttpRequest req,
            [CosmosDB("serverlesspushdb", "data", CollectionThroughput = 400,
                ConnectionStringSetting = "DefaultCosmosDB",
                CreateIfNotExists = true,
                PartitionKey = "/sensorId")] IAsyncCollector<Data> output,
            ILogger log)
        {
            var dataArray = JsonConvert.DeserializeObject<Data[]>(await req.ReadAsStringAsync());
            log.LogInformation($"{dataArray.Length} items received.");
            foreach (var data in dataArray)
            {
                await output.AddAsync(data);
            }

            return new AcceptedResult();
        }
    }
}
