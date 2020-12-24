using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Data.Entities
{
    public class ItemImageMapping
    {
        [Key]
        public int ID { get; set; }

        public Guid OwnerId { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int ItemImageId { get; set; }

        public virtual ItemImage ItemImage { get; set; }
    }
}
