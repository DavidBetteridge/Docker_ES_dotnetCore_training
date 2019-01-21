using Newtonsoft.Json;
using System;
using System.Net.Http;
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
            client.BaseAddress = new Uri("http://localhost:8700");

            var transactions = await client.GetAsync("/api/Transactions?TransactionDate=2019-01-20T00:00:00");
            var json = await transactions.Content.ReadAsStringAsync();
            transactions.EnsureSuccessStatusCode();
            var withdrawns = JsonConvert.DeserializeObject<Transaction[]>(json);

            var atms = new ATMS(client);
            foreach (var item in withdrawns)
            {
                var atm = await atms.Find(item.Atmid);
                item.Latitude = atm.Latitude;
                item.Longtitude = atm.Longtitude;
            }

            // Post into elastic

            Console.WriteLine("Hello World!");
        }

        private static void LoadPosts(Transaction[] transactions, HttpClient client)
        {
            var sb = new StringBuilder();
            foreach (var transaction in transactions)
            {
                sb.AppendLine(@"{ ""create"": { ""_index"": ""banktransaction"", ""_type"": ""post"", ""_id"": """ + transaction.id + @""" }}");

                var payload = JsonConvert.SerializeObject(transaction);
                sb.AppendLine(payload);
            }

            var content = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
            var result = client.PostAsync("/_bulk", content).Result;
            var what = result.Content.ReadAsStringAsync().Result;
        }
    }
}
