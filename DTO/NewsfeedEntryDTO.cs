using System;

namespace NewsfeedCoreMVC.DTO
{
    public class NewsfeedEntryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
