using System;
using System.Collections.Generic;
using System.Text;

namespace Load_Bank_Data
{
    class Transaction
    {
        public string Atmid { get; set; }
        public string AccountNumber { get; set; }
        public DateTime TransactionDateAndTimeUTC { get; set; }
        public decimal Amount { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
    }
}
