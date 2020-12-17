using Borrowee.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Models.TransactionModels
{
    public class TransactionListItem
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public Borrower Borrower { get; set; }

        [Display(Name = "Date Lent Out")]
        public DateTimeOffset LentOutDateUtc { get; set; }

        [Display(Name = "Date Returned")]
        public DateTimeOffset? ReturnDateUtc { get; set; }

        [Display(Name = "Is Returned")]
        public bool IsReturned { get; set; } = false;
    }
}
