using Etrade.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Data.Models.ViewModels
{
    public class ListViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; }
        public ListViewModel()
        {
            Products = new List<Product>();
        }
    }
}
