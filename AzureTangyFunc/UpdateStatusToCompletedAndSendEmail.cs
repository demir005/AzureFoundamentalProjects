using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureFunctionTangyWeb.Models;
using AzureTangyFunc.Data;
using AzureTangyFunc.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureTangyFunc
{
    public class UpdateStatusToCompletedAndSendEmail
    {
        private readonly AzureDbContext _db;

        public UpdateStatusToCompletedAndSendEmail(AzureDbContext db)
        {
            _db = db;
        }

        //[FunctionName("UpdateStatusToCompletedAndSendEmail")]
        //public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        //    [SendGrid(ApiKey = "CustomSenderGridKeyAppSettingName")] IAsyncCollector<SendGridMessage> messageCollector, ILogger log)
        //{

        //    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        //    IEnumerable<SalesRequests> salesRequestFromDb = _db.SalesRequests.Where(u => u.Status == "Image Process");
        //    foreach (var salesRequest in salesRequestFromDb)
        //    {
        //        salesRequest.Status = "Complete";
        //    }
        //    _db.UpdateRange(salesRequestFromDb);
        //    _db.SaveChanges();

        //    var message = new SendGridMessage();
        //    message.AddTo("demirmahmutovic@hotmail.com");
        //    message.AddContent("text/html", $"Proccessing completed for {salesRequestFromDb.Count()} records");
        //    message.SetFrom(new EmailAddress("demirmah93@gmail.com"));
        //    message.SetSubject("Azure Web Services Processing Successfull");
        //    await messageCollector.AddAsync(message);
        //}
    }
}
