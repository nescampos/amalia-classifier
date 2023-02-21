using Amalia.Models;
using Amalia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Amalia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataService _dataService;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, DataService dataService, IConfiguration configuration)
        {
            _logger = logger;
            _dataService = dataService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await _dataService.SaveMessage(_configuration, "Demo", @"Hello, I want to buy your AI service, but the website doesn't appear the price.
Can I pay for that?

Thanks.", "n.campos.rojas@gmail.com", "nestor@techgethr.com", "nestor@techgethr.com");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult ListMessages(int? categoryId)
        {
            ListMessagesViewModel model = new ListMessagesViewModel(_dataService, categoryId, User.Identity.Name);

            return View(model);
        }

        [Authorize]
        public IActionResult ViewMessage(long id)
        {
            ViewMessageModel model = new ViewMessageModel(_dataService, id);
            return View(model);
        }

        [Authorize]
        public IActionResult Categories()
        {
            CategoriesViewModel model = new CategoriesViewModel(_dataService, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult Categories(string newCategory)
        {
            _dataService.SaveCategory(newCategory, User.Identity.Name);
            return RedirectToAction("Categories");
        }

        [Authorize]
        public IActionResult CategoryDetail(int id)
        {
            CategoryDetailViewModel model = new CategoryDetailViewModel(_dataService, id);
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryDetail(int id, string newExample)
        {
            _dataService.SaveCategoryExample(id, newExample);
            return RedirectToAction("CategoryDetail", new { id });
        }
    }
}