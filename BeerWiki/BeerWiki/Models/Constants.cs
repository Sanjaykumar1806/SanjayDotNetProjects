using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerWiki.Models
{
    public class Constants
    {
        string search;
        string beerName;
        string type;
        string key;
        string baseUrl;

        public Constants()
        {
            this.baseUrl = System.Configuration.ConfigurationManager.AppSettings["BreweryDbApi"];
            this.search = "/search?q=";
            this.type = "&type=beer";
            this.beerName = "";
            this.key = "key=ee8a1a84bc76fd7d7ae6dd0dc45583e3";
        }
        public string BaseURL
        {
            get
            {
                return this.baseUrl;
            }
        }
        public string Type
        {
            get
            {
                return this.type;
            }
        }
        public string Search
        {
            get
            {
                return this.search;
            }
        }
        public string BeerName
        {
            get
            {
                return this.beerName;
            }
        }
        public string Key
        {
            get
            {
                return this.key;
            }
        }
    }
}