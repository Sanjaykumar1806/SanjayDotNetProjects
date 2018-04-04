using BeerWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;
using BeerWiki.Helpers;
using System.Threading.Tasks;
using System.Configuration;

namespace BeerWiki.REST
{
    public class BreweryDbAPICall
    {
        public static string ApplicationKey { get; private set; }
        public static readonly string BaseAddress = ConfigurationManager.AppSettings["BreweryDbApi"];
        public BeerResource<BeerDataModel> Beers { get; set; }

        public BreweryDbAPICall(string key, Func<HttpClient> httpClientFactory = null)
        {
            ApplicationKey = key;

            if (httpClientFactory != null)
                JsonDownloader.HttpClientFactory = httpClientFactory;

            Beers = new BeerResource<BeerDataModel>(this);
        }
    }
}