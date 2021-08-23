﻿using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Services
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;
        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection");

        }
        public Task<string> DeleteFile(string fileRoute, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = container.GetBlockBlobReference(fileName);
            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            blob.Properties.ContentType = contentType;
            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}
