using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ServerlessPushApp
{
    public class SignalRFunction
    {
        [FunctionName("Negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Function, Route = "negotiate")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "alert")]SignalRConnectionInfo signalRConnectionInfo) =>
            signalRConnectionInfo;
    }
}
