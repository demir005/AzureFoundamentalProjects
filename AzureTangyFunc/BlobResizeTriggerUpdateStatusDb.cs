using System;
using System.IO;
using System.Linq;
using AzureFunctionTangyWeb.Models;
using AzureTangyFunc.Data;
using AzureTangyFunc.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureTangyFunc
{
    public class BlobResizeTriggerUpdateStatusDb
    {
        private readonly AzureDbContext _db;

        public BlobResizeTriggerUpdateStatusDb(AzureDbContext db)
        {
            _db = db;
        }

        [FunctionName("BlobResizeTriggerUpdateStatusDb")]
        public void Run([BlobTrigger("functionsalesrep-sm/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var fileName = Path.GetFileNameWithoutExtension(name);
            SalesRequests salesRequestFromDb = _db.SalesRequests.FirstOrDefault(u => u.Id == fileName);
            if (salesRequestFromDb != null)
            {
                salesRequestFromDb.Status = "Image Processed";
                _db.SalesRequests.Update(salesRequestFromDb);
                _db.SaveChanges();
            }
        }
    }
}
