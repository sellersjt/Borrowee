using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Models.BorrowerModels
{
    public class BorrowerCreate
    {
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
