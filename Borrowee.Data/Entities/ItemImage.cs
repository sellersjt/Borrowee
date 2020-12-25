using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Data.Entities
{
    public class ItemImage
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }

        [Display(Name = "File")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string FileName { get; set; }
    }
}
