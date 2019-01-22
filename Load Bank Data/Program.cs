using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Load_Bank_Data
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Get transactions for yesterday
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8111");

            var transactions = await client.GetAsync("/api/Transactions?TransactionDate=2019-01-20T00:00:00");
            var json = await transactions.Content.ReadAsStringAsync();
            transactions.EnsureSuccessStatusCode();
            var withdrawns = JsonConvert.DeserializeObject<Transaction[]>(json);

            var atms = new ATMS(client);
            foreach (var item in withdrawns)
            {
                var atm = await atms.Find(item.Atmid);
                item.location = new Location()
                {
                    lat = atm.Latitude,
                    lon = atm.Longitude,
                };
            }

            // Post into elastic
            await LoadPosts(withdrawns);

            Console.WriteLine("Job Done!");
        }

        private static async Task LoadPosts(Transaction[] transactions)
        {
            var sb = new StringBuilder();
            foreach (var transaction in transactions)
            {
                sb.AppendLine(@"{ ""create"": { ""_index"": ""banktransaction"", ""_type"": ""post"", ""_id"": """ + transaction.ID + @""" }}");

                var payload = JsonConvert.SerializeObject(transaction);
                sb.AppendLine(payload);
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9200");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var content = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/_bulk", content);
            var what = await result.Content.ReadAsStringAsync();
        }
    }
}
