using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Data.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey(nameof(Borrower))]
        public int BorrowerId { get; set; }
        public virtual Borrower Borrower { get; set; }

        [Display(Name = "Date Lent Out")]
        public DateTimeOffset LentOutDateUtc { get; set; }

        [Display(Name = "Date Returned")]
        public DateTimeOffset ReturnDateUtc { get; set; }

        [Display(Name = "Is Returned")]
        public bool IsReturned { get; set; }
    }
}
