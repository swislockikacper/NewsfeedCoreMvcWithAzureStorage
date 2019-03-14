using NewsfeedCoreMVC.DTO;
using System.Collections.Generic;

namespace NewsfeedCoreMVC.Models
{
    public class NewsfeedEntryViewModel
    {
        public NewsfeedEntryModel Model { get; set; }
        public IEnumerable<NewsfeedEntryDTO> Entries { get; set; }
    }
}
