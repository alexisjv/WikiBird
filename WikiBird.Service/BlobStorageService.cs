using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBird.Service
{
    public interface IBlobStorageService
    {
        public Task<List<string>> GetBlobAsync();

        public Task<string> UploadBlobAsync(string blobName, Stream stream);

        public Task DeleteBlobAsync(string blobName);
    }

    public class BlobStorageService : IBlobStorageService
    {
        private BlobServiceClient _client;

        private BlobContainerClient _containerClient;

        public BlobStorageService(BlobServiceClient client)
        {
            _client = client;
            _containerClient = client.GetBlobContainerClient("birds");
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            await _containerClient.DeleteBlobAsync(blobName);
        }

        public async Task<List<string>> GetBlobAsync()
        {
            List<string> blobs = new List<string>();
            await foreach (BlobItem blob in _containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }
            return blobs;
        }

        public async Task<string> UploadBlobAsync(string blobName, Stream stream)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(stream, overwrite: true);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}

