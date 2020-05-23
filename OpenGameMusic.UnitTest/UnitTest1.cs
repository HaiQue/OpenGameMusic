//using DocumentFormat.OpenXml.Office2010.Excel;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Moq;
using OpenGameMusic.Controllers;
using OpenGameMusic.Models;
using OpenGameMusic.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace OpenGameMusic.UnitTest
{
    public class UnitTest1
    {
        private readonly HomeController homeController;
        private readonly ISongRepository songRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel();

        //[TestInitialize()]
        //public void Setup()
        //{
        //    Mock<ISongRepository> songRepo = new Mock<ISongRepository>();
        //    Mock<IFileProvider> fileProvider = new Mock<IFileProvider>();
        //    Mock<IHostingEnvironment> hostingEnviroment = new Mock<IHostingEnvironment>();
        //    homeController = new HomeController(songRepo.Object, fileProvider.Object, hostingEnviroment.Object);
        //}

        public UnitTest1()
        {
            homeController = A.Fake<HomeController>();
            songRepository = A.Fake<ISongRepository>();
            hostingEnvironment = A.Fake<IHostingEnvironment>();
        }

        [Fact]
        public void CreateHomeDetailsViewModelTest()
        {
            const int id = 1;

            Song song = new Song
            {
                Id = 1,
                Artist = "TestArtist",
                SongName = "TestSong",
                License = Dept.MasterLicense,
                PhotoPath = "TestPhotoPath"
            };

            var callToSongRepository = A.CallTo(() => songRepository.GetSong(id));
            callToSongRepository.Returns(song);

            var result = homeController.CreateHomeDetailsViewModel(id);
            Assert.IsType<HomeDetailsViewModel>(result);
        }

        [Fact]
        public void EditPostTest()
        {
            SongEditViewModel model = new SongEditViewModel
            {
                Id = 1,
                ExistingPhotoPath = "TestPath",
                Artist = "TestArtist",
                SongName = "TestSongName",
                License = Dept.MasterLicense,
                Photo = null
            };

            Song song = new Song
            {
                Id = 1,
                Artist = "TestArtist",
                SongName = "TestSong",
                License = Dept.MasterLicense,
                PhotoPath = "TestPhotoPath"
            };

            var callToHosting = A.CallTo(() => hostingEnvironment.WebRootPath);
            callToHosting.Returns("FakePath");
            var callToRepo = A.CallTo(() => songRepository.Update(song));
            callToRepo.Returns(song);
            var callToSongRep = A.CallTo(() => songRepository.GetSong(1));
            callToSongRep.Returns(song);
            A.CallTo(homeController).WithReturnType<string>().Returns("FakeUniueFileName");

            homeController.Edit(model);
        }

        [Fact]
        public void EditGetTest()
        {
            Song song = new Song
            {
                Id = 1,
                Artist = "TestArtist",
                SongName = "TestSong",
                License = Dept.MasterLicense,
                PhotoPath = "TestPhotoPath"
            };

            var callToRepo = A.CallTo(() => songRepository.GetSong(1));
            callToRepo.Returns(song);

            var result = homeController.Edit(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void Test2()
        {
            const int id = 1;
            
            homeDetailsViewModel.Song = new Song()
            {
                Artist = "Faustix",
                SongName = "Solo",
                License = Dept.PublicPerformanceLicense,
                PhotoPath = "d8ed6a7e-a59f-46e2-88cf-44bf3293a429_food-vegetables-nature-red-128245.jpg",
            };
            homeDetailsViewModel.PageTitle = "Song Details";

            HomeDetailsViewModel homeDetailsViewModel1 = homeController.CreateHomeDetailsViewModel(id);
            Console.WriteLine(homeDetailsViewModel1.PageTitle);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(homeController.CreateHomeDetailsViewModel(id), homeDetailsViewModel);

        }

        [Fact]
        public void Test3()
        {


        }
    }
}
