using System;
using System.Collections.Generic;
using System.Text;

namespace Load_Bank_Data
{
    class Transaction
    {
        public long ID { get; set; }
        public string Atmid { get; set; }
        public string AccountNumber { get; set; }
        public DateTime TransactionDateAndTimeUTC { get; set; }
        public decimal Amount { get; set; }
        public Location location { get; set; }
    }

    class Location
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
}
