using Common.Lib.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Lib.Entities.InputModels
{
    public partial class JournalInput : IEntity
    {
        [Required]
        [StringLength(50)]
        public string TransactionIdentifier { get; set; }
        public decimal Amount { get; set; }
        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
         
    }
}
