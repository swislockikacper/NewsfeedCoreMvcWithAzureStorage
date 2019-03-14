using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NewsfeedCoreMVC.Abstract
{
    public interface IBlobStorageService
    {
        Task UploadFile(IFormFile file, string fileName);
    }
}
