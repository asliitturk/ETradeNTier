using Etrade.Data.Models.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Data.Models.Entities
{
    public class Category : BaseClass
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Product> Products { get; set; }
        public Category()
        { 
            Products = new List<Product>();
        }
    }
}
