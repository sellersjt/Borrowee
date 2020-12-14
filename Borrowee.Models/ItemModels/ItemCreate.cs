using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Models.ItemModels
{
    public class ItemCreate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ModelNumber { get; set; }

        public string SerialNumber { get; set; }

        public decimal Value { get; set; }
    }
}
