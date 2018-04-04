using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerWiki.Helpers;
using BeerWiki.Models;

namespace BeerWiki.REST
{
    public class BeerResource<T>
    {
        private BreweryDbAPICall client;

        public BeerResource(BreweryDbAPICall breweryApiClient)
        {
            client = breweryApiClient;
        }

        public async Task<ResponseContainer<List<T>>> GetAll()
        {
            return await GetAll(1);
        }

        public async Task<ResponseContainer<List<T>>> GetAll(int pageNumber)
        {
            var url = $"{BreweryDbAPICall.BaseAddress}beers?p={pageNumber}&withBreweries=y&key={BreweryDbAPICall.ApplicationKey}&format=json";
            return await JsonDownloader.DownloadSerializedJsonDataAsync<ResponseContainer<List<T>>>(url);            
        }

        public async Task<ResponseContainer<T>> Get(string id)
        {
            var url = $"{BreweryDbAPICall.BaseAddress}beer/{id}?withBreweries=y&key={BreweryDbAPICall.ApplicationKey}&format=json";
            return await JsonDownloader.DownloadSerializedJsonDataAsync<ResponseContainer<T>>(url);
        }

        public async Task<ResponseContainer<List<T>>> GetBeerListWithFilter(string filterStr)
        {
            var url = $"{BreweryDbAPICall.BaseAddress}beers?{filterStr}&key={BreweryDbAPICall.ApplicationKey}&format=json";
            return await JsonDownloader.DownloadSerializedJsonDataAsync<ResponseContainer<List<T>>>(url);
        }

        public async Task<ResponseContainer<List<T>>> Search(string keyword)
        {
            var url = $"{BreweryDbAPICall.BaseAddress}search?q={keyword}&type=beer&withBreweries=y&withSocialAccounts=y&withGuilds=y&withLocations=y&withAlternateNames=y&hasLabels=y&withIngredients=y&key={BreweryDbAPICall.ApplicationKey}&format=json";
            return await JsonDownloader.DownloadSerializedJsonDataAsync<ResponseContainer<List<T>>>(url);
        }
    }
}
