using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Models.ItemImageModels
{
    public class ItemImageDetail
    {
        public int Id { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
    }
}
