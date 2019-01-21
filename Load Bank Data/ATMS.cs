using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Load_Bank_Data
{
    class ATMS
    {
        private readonly HttpClient _client;
        private readonly Dictionary<string, ATM> _cache;

        public ATMS(HttpClient client)
        {
            _client = client;
            _cache = new Dictionary<string, ATM>();
        }
        public async Task<ATM> Find(string ATMID)
        {
            if (!_cache.TryGetValue(ATMID, out var atm))
            {
                var atms = await _client.GetAsync($"/api/ATMs/{ATMID}");
                var atmsJson = await atms.Content.ReadAsStringAsync();
                atms.EnsureSuccessStatusCode();
                atm = JsonConvert.DeserializeObject<ATM>(atmsJson);
                _cache.Add(ATMID, atm);
            }
            return atm;
        }
    }
}
