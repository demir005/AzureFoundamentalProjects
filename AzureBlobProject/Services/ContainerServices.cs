﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerServices : IContainerService
    {
        private readonly BlobServiceClient _blobClient;

        public ContainerServices(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerName = new();
            await foreach (BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                containerName.Add(blobContainerItem.Name);
            }
            return containerName;
        }

        public async Task<List<string>> GetAllContainerAndBlob()
        {
            List<string> containerAndBlobName = new();
            containerAndBlobName.Add("Account Name : " + _blobClient.AccountName);
            containerAndBlobName.Add("------------------------------------------");
            await foreach(BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                containerAndBlobName.Add("--" + blobContainerItem.Name);
                BlobContainerClient blobContainer =
                    _blobClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach(BlobItem blobItem in blobContainer.GetBlobsAsync())
                {
                    containerAndBlobName.Add("------" + blobItem.Name);
                }
                containerAndBlobName.Add("------------------------------------------");
            }
            return containerAndBlobName;
        }
    }
}
