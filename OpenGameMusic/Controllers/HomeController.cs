using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenGameMusic.Models;


using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using OpenGameMusic.ViewModels;

namespace OpenGameMusic.Controllers
{
    // [Authorize] jeigu reikia authorization, bet home tegul gali eiti
    public class HomeController : Controller
    {

        /*        private readonly ILogger<HomeController> _logger;

                public HomeController(ILogger<HomeController> logger)
                {
                    _logger = logger;
                }*/

        // test

        private readonly ISongRepository _songRepository;
        private readonly IFileProvider fileProvider;

        public HomeController(ISongRepository songRepository, IFileProvider fileProvider)
        {
            _songRepository = songRepository;
            this.fileProvider = fileProvider;
        }

        public ViewResult Index2()
        {
            var model = _songRepository.GetAllSongs();
            return View(model);
        }


        public ViewResult Details()
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Song = _songRepository.GetSong(1),
                PageTitle = "Song Details"
            };
            Song model = _songRepository.GetSong(1);
            //ViewBag.Song = model;
            ViewBag.PageTitle = "Song Details";

            return View(homeDetailsViewModel);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadMusic()
        {
            return View();
        }

/*        private readonly IFileProvider fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
*/
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot//music",
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }


        public IActionResult Files()
        {
            var model = new FileViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails {Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot//music", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".aac", "audio/aac"},
                {".wav", "audio/wav"},
                {".flac", "audio/flac"},
                {".mp3", "audio/mp3"}
            };
        }

    }
}
