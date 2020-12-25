using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Borrowee.Models.ItemModels
{
    public class CreateItemViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        public decimal Value { get; set; }

        public int ItemImageId { get; set; }

        public IEnumerable<SelectListItem> Images { get; set; }
    }
}
