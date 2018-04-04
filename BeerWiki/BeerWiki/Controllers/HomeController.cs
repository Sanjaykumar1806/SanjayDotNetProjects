using BeerWiki.Models;
using BeerWiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerWiki.REST;
using System.Threading.Tasks;
using System.Configuration;
using BeerWiki.Helpers;

namespace BeerWiki.Controllers
{
    [CustomExceptionFilter]
    public class HomeController : Controller
    {
        private readonly BreweryDbAPICall client = new BreweryDbAPICall(ConfigurationManager.AppSettings["BreweryDbApiKey"]);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BeerDetails(string Id)
        {
            var response = await client.Beers.Get(Id);
            BeerDataModel beer = response.Data;

            return View(beer);
        } 
        
        [HttpPost]
        public async Task<ActionResult> SearchBeerAjaxCall(string searchBy, string searchText)
        {
            try
            {
                List<BeerDataModel> beerDataModel = new List<BeerDataModel>();
                int page = 1, totalRecords = 0;
                if (searchBy == "ID" && searchText != "" && searchText != null)
                {
                    var response = await client.Beers.Get(searchText);
                    BeerDataModel model = response.Data;
                    beerDataModel.Add(response.Data);
                    totalRecords = response.TotalResults;
                }
                else if (searchBy == "Name" && searchText != "" && searchText != null)
                {
                    var response = await client.Beers.Search(searchText);
                    beerDataModel = response.Data.ToList();
                    totalRecords = response.TotalResults;
                }
                else
                {
                    var response = await client.Beers.GetAll(1);
                    beerDataModel = response.Data.ToList();
                    totalRecords = response.TotalResults;
                }
                var Results = beerDataModel.Select(
                    beerItem => new
                    {
                        beerItem.Id,
                        beerItem.Name,
                        beerItem.Description,
                        beerItem.Abv,
                        beerItem.IBU,
                        beerItem.StyleId,
                        beerItem.Status,
                        beerItem.CreateDate,
                        beerItem.UpdateDate,
                        IsOrganic = beerItem.IsOrganic == "Y" ? "Yes" : "No",
                    CategoryName = beerItem.Style.Category.Name == null ? "" : beerItem.Style.Category.Name,
                    }).ToList();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)50);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = Results
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult BeerGridList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllBeerList(string sort, int page, int rows, string searchString)
        {
            try
            {
                var response = await client.Beers.GetAll(page);
                List<BeerDataModel> dataModel = new List<BeerDataModel>();
                dataModel = response.Data.ToList();
                
                var selectedBeerList = dataModel.Select(
                    beerItem => new
                    {
                        beerItem.Id,
                        beerItem.Name,
                        beerItem.Description,
                        beerItem.Abv,
                        beerItem.IBU,
                        beerItem.StyleId,
                        beerItem.Status,
                        beerItem.CreateDate,
                        beerItem.UpdateDate,
                        IsOrganic = beerItem.IsOrganic == "Y" ? "Yes" : "No",
                        CategoryName = beerItem.Style.Category.Name == null ? "" : beerItem.Style.Category.Name,
                    }).ToList();

                int totalRecords = response.TotalResults;
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows); 
                if (sort.ToUpper() == "DESC")
                {
                    selectedBeerList = selectedBeerList.OrderByDescending(s => s.Id).ToList();
                }
                else
                {
                    selectedBeerList = selectedBeerList.OrderBy(s => s.Id).ToList();
                }                
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = selectedBeerList
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetFilteredBeerList(int page, string filterStr)
        {
            try
            {
                var response = await client.Beers.GetBeerListWithFilter(filterStr);
                List<BeerDataModel> beerDataModel = new List<BeerDataModel>();                
                beerDataModel = response.Data;
                int totalRecords = beerDataModel.Count();
                var Results = beerDataModel.Select(
                    beerItem => new
                    {
                        beerItem.Id,
                        beerItem.Name,
                        beerItem.Description,
                        beerItem.Abv,
                        beerItem.IBU,
                        beerItem.StyleId,
                        beerItem.Status,
                        beerItem.CreateDate,
                        beerItem.UpdateDate,
                        IsOrganic = beerItem.IsOrganic == "Y" ? "Yes" : "No",
                        CategoryName = beerItem.Style.Category.Name == null ? "" : beerItem.Style.Category.Name,
                    }).ToList();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)50);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = Results
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}