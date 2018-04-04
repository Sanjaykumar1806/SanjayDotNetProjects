using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerWiki;
using BeerWiki.Controllers;
using System.Threading.Tasks;
using System.Configuration;
using BeerWiki.REST;

namespace BeerWiki.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly BreweryDbAPICall client = new BreweryDbAPICall(ConfigurationManager.AppSettings["BreweryDbApiKey"]);

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void BeerDetails()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult result = await controller.BeerDetails("c5A6lY") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void SearchBeerAjaxCall()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            JsonResult result = await controller.SearchBeerAjaxCall("Name", "2 Pencil") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BeerGridList()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void GetAllBeerList()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            JsonResult result = await controller.GetAllBeerList("", 1, 50, "") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void GetFilteredBeerList()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            JsonResult result = await controller.GetFilteredBeerList(1, "&ids=1qDm69,1TyUeu&isOrganic=N") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
