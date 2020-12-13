using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Data.Entities
{
    public class Borrower
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
