﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeerWiki.Helpers
{
    public static class JsonDownloader
    {
        public static Func<HttpClient> HttpClientFactory { get; set; } = new Func<HttpClient>(() => new HttpClient());

        public static async Task<T> DownloadSerializedJsonDataAsync<T>(string url) where T : new()
        {
            using (var httpClient = HttpClientFactory())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = string.Empty;
                try
                {
                    jsonData = await httpClient.GetStringAsync(url);
                }
                catch (Exception)
                {
                    return default(T);
                }
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : default(T);
            }
        }
    }
}
