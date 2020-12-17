using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Borrowee.Models.TransactionModels
{
    public class EditTransactionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [Display(Name = "Borrower Id")]
        public int BorrowerId { get; set; }

        [Display(Name = "Date Lent Out")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTimeOffset LentOutDateUtc { get; set; }

        [Display(Name = "Date Returned")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTimeOffset? ReturnDateUtc { get; set; }

        [Display(Name = "Is Returned")]
        public bool IsReturned { get; set; } = false;

        public IEnumerable<SelectListItem> Items { get; set; }

        public IEnumerable<SelectListItem> Borrowers { get; set; }
    }
}
