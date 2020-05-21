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
using Microsoft.AspNetCore.Hosting;

namespace OpenGameMusic.Controllers
{
    // [Authorize] jeigu reikia authorization, bet home tegul gali eiti
    public class HomeController : Controller
    {

        private readonly ISongRepository _songRepository;
        private readonly IFileProvider fileProvider;

        private readonly ILogger<HomeController> _logger;

        private readonly IHostingEnvironment hostingEnvironment;


        public HomeController(ISongRepository songRepository, IFileProvider fileProvider,
                               IHostingEnvironment hostingEnvironment)
        {
            _songRepository = songRepository;
            this.fileProvider = fileProvider;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index2()
        {
            var model = _songRepository.GetAllSongs();
            return View(model);
        }


        public ViewResult Details(int? id) // int id
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Song = _songRepository.GetSong(id ?? 1), // id
                PageTitle = "Song Details"
            };
            //Song model = _songRepository.GetSong(id); //  id
            //ViewBag.Song = model;
            ViewBag.PageTitle = "Song Details";

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id) // public ViewResult Edit (int id)
        {
            Song song = _songRepository.GetSong(id);
            SongEditViewModel songEditViewModel = new SongEditViewModel
            {
                Id = song.Id,
                Artist = song.Artist,
                SongName = song.SongName,
                License = song.License,
                ExistingPhotoPath = song.PhotoPath
            };
            return View(songEditViewModel);
        }


        [HttpPost]
        public IActionResult Edit(SongEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Song song = _songRepository.GetSong(model.Id);
                song.Artist = model.Artist;
                song.SongName = model.SongName;
                song.License = model.License;
                string uniqueFileName = ProcessUploadedFile(model);

                //string uniqueFileName = null;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                             "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    song.PhotoPath = ProcessUploadedFile(model);

                }
                Song newSong = new Song
                {
                    Artist = model.Artist,
                    SongName = model.SongName,
                    License = model.License,
                    PhotoPath = uniqueFileName
                };


                Song updatedSong = _songRepository.Update(song);
                return RedirectToAction("index2");
            }

            return View();
        }

        private string ProcessUploadedFile(SongCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult UploadMusic(SongCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = ProcessUploadedFile(model);

                Song newSong = new Song
                {
                    Artist = model.Artist,
                    SongName = model.SongName,
                    License = model.License,
                    PhotoPath = uniqueFileName
                };

                _songRepository.Add(newSong);
                return RedirectToAction("Details", new { id = newSong.Id });
            }

            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadMusic()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 25000000)]  // we support 25 mb  // 409715200
        [RequestSizeLimit(25000000)]
        public IActionResult Upload(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string fileName = $"{webHostEnvironment.WebRootPath}" +
                    $"\\UploadedFiles\\{file.FileName}";

            // file.Filter = "mp3|*.mp3|All Files|*.*";

            string[] AcceptableMusicFileTypes = { ".flac", ".aac", ".wav", ".mp3", ".m4a" };
            bool IsFileValidFormat = false;

            for (int i = 0; i < AcceptableMusicFileTypes.Length; i++)
            {
                if (file.FileName.EndsWith(AcceptableMusicFileTypes[i]))
                {
                    IsFileValidFormat = true;
                    ViewData["message"] = "Your music file is uploaded succesfuly.";
                    using (FileStream fileStream = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
            }

            if (IsFileValidFormat == false)
            {
                ViewData["message"] = $"File is not acceptable format, This platform supports: " +
                   $".flac, .aac, .wav, .mp3, .m4a types of music files. Your file name is: {file.FileName }";
            }

            return View("Index");
        }

        [HttpGet]
        public IActionResult ListOfFiles()
        {
            var model = new FileViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DownloadMusic(string fileName)
        {
            if (fileName == null)
                return Content("file Name is not present");

            var filePath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot//UploadedFiles", fileName);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }
    }
}
