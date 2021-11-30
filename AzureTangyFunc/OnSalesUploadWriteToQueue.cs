using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureTangyFunc.Models;

namespace AzureTangyFunc
{
    public static class OnSalesUploadWriteToQueue
    {
        [FunctionName("OnSalesUploadWriteToQueue")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("SalesRequestInBound", Connection = "AzureWebJobsStorage")] IAsyncCollector<SalesRequests> salesRequestQueue,
            ILogger log)
        {
            log.LogInformation("Sales request recived by OnSalesUploadWriteToQueue function");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SalesRequests data = JsonConvert.DeserializeObject<SalesRequests>(requestBody);

            await salesRequestQueue.AddAsync(data);

            string responseMessage = "Sales Request has been recived for ." + data.Name;
            return new OkObjectResult(responseMessage);
        }
    }
}
