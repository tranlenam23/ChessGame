using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace ChessGame
{
    public class AzureBlobService
    {
        public readonly string _storageAccount = "storageofnam";
        public readonly string _accessKey = "a2UUYwmjlU6Un/AQBfoKTQetTBkbKKTRLdecm3++QmpbpJzB+mjdCgcAlyFvz0FQ5g98qMiZPNYD+AStwOJFag==";
        public readonly BlobServiceClient _blobServiceClient;

        public AzureBlobService()
        {
            var credential = new StorageSharedKeyCredential(_storageAccount, _accessKey);
            var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }
        public async Task ListBlobContainersAsync()
        {
            var containers = _blobServiceClient.GetBlobContainersAsync();
            await foreach(var container in containers)
            {
                Console.WriteLine(container.Name);
            }
        }
        [HttpPost]
        public async Task<List<Uri>> UploadFilesAsync(string filePath)
        {
            var blobUris = new List<Uri>();
            var blobContainer = _blobServiceClient.GetBlobContainerClient("files");
            var Image = blobContainer.GetBlobClient("Image/" + Path.GetFileName(filePath));
            await Image.UploadAsync(filePath, true);
            blobUris.Add(Image.Uri);
            return blobUris; 
        }
    }
}
