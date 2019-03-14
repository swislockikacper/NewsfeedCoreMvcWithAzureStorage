using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewsfeedCoreMVC.Abstract;
using NewsfeedCoreMVC.Constants;

namespace NewsfeedCoreMVC.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;
        private readonly CloudBlobContainer blobContainer;
        private readonly IConfiguration configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            this.configuration = configuration;
            storageAccount = CloudStorageAccount.Parse(configuration.GetValue<string>("AzureStorage:ConnectionString"));
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(BlobStorage.NewsfeedBlobContainer);
        }

        public async Task UploadFile(IFormFile file, string fileName)
        {
            if (file == null)
                return;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                await UploadToBlob(fileName, memoryStream.ToArray());
            }
        }

        private async Task UploadToBlob(string fileName, byte[] file)
        {
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(fileName);

            if (file != null)
                await cloudBlockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
        }
    }
}
