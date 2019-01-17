using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetATMs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://linkservicesapi.azurewebsites.net");

            var parameters = new
            {
                CentreLatitude = 54.2036094665527,
                CentreLongtitude = -1.36397504806519,
                SouthLatitude = 50.6208190917969,
                WestLongtitude = -2.56476998329163,
                NorthLatitude = 58.647590637207,
                EastLongtitude = -0.21100999414920807,
                Currency = "All",
                Mobile = false,
                Wheelchair = false,
                Audio = false,
                PINManagement = false,
                FiverNotes = false,
                OnlyFreeToUse = false,
                DepositNotes = false,
                DepositCoins = false,
                DepositForeignCurrencies = false,
                AdvancedSearch = false,
                ZoomLevel = 15,
                ATMId = "yorkshire",
                ATMIdSearch = false
            };

            var data = await client.PostAsJsonAsync("api/atm/searcharoundpoint", parameters);
            //var json = await data.Content.ReadAsStringAsync();
            var results = await data.Content.ReadAsJsonAsync<Result>();

            var json = JsonConvert.SerializeObject(results);
            //File.WriteAllText("Yorkshire.atms.json", json);

        }
    }

    public class Result
    {
        public IEnumerable<SearchResult> SearchResults { get; set; }
    }
    public class SearchResult
    {
        public string ATMId { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
    }
}
