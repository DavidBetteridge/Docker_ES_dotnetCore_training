using System;

namespace BankData.ViewModels
{
    public class CashWithdrawalViewModel
    {
        public string ATMID { get; set; }
        public string AccountNumber { get; set; }
        public DateTime TransactionDateAndTimeUTC { get; set; }
        public decimal Amount { get; set; }

    }
}