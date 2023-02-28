using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Model;
using System.Web;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace CatalogSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public IActionResult Index(int id = 0)
        {
            var model = new FoldersModel();

            if( id == 0)
            {
                model = _catalogService.GetParentsFolders();
                return View("ParentFolders", model);
            }
            
            model = _catalogService.GetFoldersById(id);
            return View(model);
        }

        public FileResult GenerateTextFile()
        {

            var folders = _catalogService.GetParentsFolders();



            var result = _catalogService.GetFoldersStructure(folders);

            var json = JsonConvert.SerializeObject(result);
            //Download the CSV file.
            byte[] fileBytes = Encoding.UTF8.GetBytes(json);

            // Возвращаем файл как результат действия контроллера
            return File(fileBytes, "text/plain", "example.txt");
        }

        

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if(file !=null) 
            { 
                var result = new StringBuilder();

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }

                var folders = JsonConvert.DeserializeObject<FoldersModel>(result.ToString());

                _catalogService.SaveFolders(folders);

            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Index");
        }

    }
}


