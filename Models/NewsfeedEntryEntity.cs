using Microsoft.WindowsAzure.Storage.Table;

namespace NewsfeedCoreMVC.Models
{
    public class NewsfeedEntryEntity : TableEntity
    {
        public NewsfeedEntryEntity()
        {
        }

        public NewsfeedEntryEntity(string name, string title)
        {
            this.PartitionKey = name;
            this.RowKey = title;
        }

        public string Text { get; set; }
        public string FileName { get; set; }
    }
}
