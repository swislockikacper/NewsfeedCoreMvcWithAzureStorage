using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NewsfeedCoreMVC.Abstract;
using NewsfeedCoreMVC.Constants;
using NewsfeedCoreMVC.DTO;
using NewsfeedCoreMVC.Models;

namespace NewsfeedCoreMVC.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableClient;
        private readonly IConfiguration configuration;
        private readonly IBlobStorageService blobService;

        public TableStorageService(IConfiguration configuration, IBlobStorageService blobService)
        {
            this.configuration = configuration;
            this.blobService = blobService;
            storageAccount = CloudStorageAccount.Parse(configuration.GetValue<string>("AzureStorage:ConnectionString"));
            tableClient = storageAccount.CreateCloudTableClient();
        }

        public void AddEntry(NewsfeedEntryModel entry)
        {
            CloudTable table = tableClient.GetTableReference(TableStorage.NewsfeedEntryTableName);
            table.CreateIfNotExistsAsync();

            var fileName = string.Empty;

            if (entry.File != null)
            {
                fileName = $"{Guid.NewGuid().ToString()}{GetFileExtension(entry.File.FileName)}";
                blobService.UploadFile(entry.File, fileName);
            }

            var newsfeedEntryEntity = new NewsfeedEntryEntity(entry.Name, entry.Title);
            newsfeedEntryEntity.Text = entry.Text;
            newsfeedEntryEntity.FileName = fileName;

            var insertOperation = TableOperation.Insert(newsfeedEntryEntity);
            table.ExecuteAsync(insertOperation);
        }

        public async Task<IEnumerable<NewsfeedEntryDTO>> GetAllEntries()
        {
            var elements = new List<NewsfeedEntryDTO>();

            CloudTable table = tableClient.GetTableReference(TableStorage.NewsfeedEntryTableName);

            var query = new TableQuery<NewsfeedEntryEntity>();
            var data = await table.ExecuteQuerySegmentedAsync<NewsfeedEntryEntity>(query, null);

            foreach (var entry in data.Results)
            {
                elements.Add(new NewsfeedEntryDTO
                {
                    Name = entry.PartitionKey,
                    Title = entry.RowKey,
                    Text = entry.Text,
                    Timestamp = entry.Timestamp,
                    FileName = entry.FileName
                });
            }

            return elements;
        }

        public async Task<IEnumerable<NewsfeedEntryDTO>> GetEntriesByName(string name)
        {
            var elements = new List<NewsfeedEntryDTO>();

            CloudTable table = tableClient.GetTableReference(TableStorage.NewsfeedEntryTableName);

            var query = new TableQuery<NewsfeedEntryEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, name));

            var data = await table.ExecuteQuerySegmentedAsync<NewsfeedEntryEntity>(query, null);

            foreach (var entry in data.Results)
            {
                elements.Add(new NewsfeedEntryDTO
                {
                    Name = entry.PartitionKey,
                    Title = entry.RowKey,
                    Text = entry.Text,
                    Timestamp = entry.Timestamp,
                    FileName = entry.FileName
                });
            }

            return elements;
        }

        private string GetFileExtension(string filename)
            => filename.Substring(filename.LastIndexOf('.'));
    }
}
