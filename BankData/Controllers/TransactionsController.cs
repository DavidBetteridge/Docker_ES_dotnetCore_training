using BankData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CashWithdrawalViewModel>> Get(DateTime TransactionDate)
        {
            if (TransactionDate > DateTime.Now) return BadRequest("Transaction date must be in the past");

            var day = (int)(DateTime.Today - new DateTime(2000, 1, 1)).TotalDays;
            var rnd = new Random(day);

            var json = System.IO.File.ReadAllText("Data/Yorkshire.atms.json");
            var atms = JsonConvert.DeserializeObject<Result>(json);

            var numberOfTranactions = rnd.Next(1000, 10000);
            var transactions = new List<CashWithdrawalViewModel>(numberOfTranactions);
            var usedNumbers = new HashSet<string>() { "10012300" };
            for (int i = 0; i < numberOfTranactions; i++)
            {
                var an = "";
                while (an == "" || usedNumbers.Contains(an))
                    an = rnd.Next(10000000, 100000000).ToString();

                transactions.Add(new CashWithdrawalViewModel()
                {
                    AccountNumber = an,
                    Amount = 5 * rnd.Next(1, 200),
                    ATMID = atms.SearchResults[rnd.Next(atms.SearchResults.Count())].ATMId,
                    TransactionDateAndTimeUTC = new DateTime(TransactionDate.Year,
                                                              TransactionDate.Month,
                                                              TransactionDate.Day,
                                                              rnd.Next(1, 24),
                                                              rnd.Next(0, 60),
                                                              rnd.Next(0, 60))
                });

                usedNumbers.Add(an);

            }


            //This card was used at all ATMs machines during the night from 2am onwards
            var time = new DateTime(TransactionDate.Year, TransactionDate.Month, TransactionDate.Day, 2, 0, 0);
            foreach (var atm in atms.SearchResults)
            {
                transactions.Add(new CashWithdrawalViewModel()
                {
                    AccountNumber = "10012300",
                    Amount = 500,
                    ATMID = atm.ATMId,
                    TransactionDateAndTimeUTC = time
                });
                time = time.AddMinutes(5);
            }

            return transactions.OrderBy(a => a.TransactionDateAndTimeUTC).ToArray();
        }

    }
}
