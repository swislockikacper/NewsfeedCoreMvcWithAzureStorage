using NewsfeedCoreMVC.DTO;
using NewsfeedCoreMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsfeedCoreMVC.Abstract
{
    public interface ITableStorageService
    {
        void AddEntry(NewsfeedEntryModel entry);
        Task<IEnumerable<NewsfeedEntryDTO>> GetAllEntries();
        Task<IEnumerable<NewsfeedEntryDTO>> GetEntriesByName(string name);
    }
}
