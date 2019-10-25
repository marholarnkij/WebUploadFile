using System;
using System.Collections.Generic;

namespace Common.Lib.Data.Models
{
    public partial class JournalDetails
    {
        public int Rid { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
