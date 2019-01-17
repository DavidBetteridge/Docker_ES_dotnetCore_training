using System;
using System.Collections.Generic;

namespace BankData.ViewModels
{
    public class Result
    {
        public SearchResult[] SearchResults { get; set; }

    }
    public class SearchResult
    {
        public string ATMId { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
    }
}
