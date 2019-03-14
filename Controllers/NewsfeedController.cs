using Microsoft.AspNetCore.Mvc;
using NewsfeedCoreMVC.Models;
using System.Threading.Tasks;
using NewsfeedCoreMVC.Abstract;

namespace NewsfeedCoreMVC.Controllers
{
    public class NewsfeedController : Controller
    {
        private readonly ITableStorageService tableStorageService;

        public NewsfeedController(ITableStorageService tableStorageService)
        {
            this.tableStorageService = tableStorageService;
        }

        public async Task<IActionResult> Index()
        {           
            var viewModel = new NewsfeedEntryViewModel()
            {
                Entries = await tableStorageService.GetAllEntries(),
                Model = new NewsfeedEntryModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult AddEntry([FromForm]NewsfeedEntryModel entry)
        {
            tableStorageService.AddEntry(entry);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName(string name)
        {
            var results = await tableStorageService.GetEntriesByName(name);

            var viewModel = new NewsfeedEntryViewModel()
            {
                Entries = results,
                Model = new NewsfeedEntryModel()
            };

            return View("Index", viewModel);
        }
    }
}
