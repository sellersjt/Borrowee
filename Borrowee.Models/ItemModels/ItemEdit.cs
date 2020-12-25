using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Models.ItemModels
{
    public class ItemEdit
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        public decimal Value { get; set; }

        [Display(Name = "Item Image")]
        public int? ItemImageId { get; set; }
    }
}
