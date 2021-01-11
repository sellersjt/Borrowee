using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Data.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ModelNumber { get; set; }

        public string SerialNumber { get; set; }

        public decimal Value { get; set; }

        public int? ItemImageId { get; set; }
        public virtual ItemImage ItemImage { get; set; }
    }
}
