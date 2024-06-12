using Etrade.Business.Concreate;
using Etrade.DAL.Abstract;
using Etrade.Data.Context;
using Etrade.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.DAL.Conscreate
{
    public class ProductDAL : GenericRepository<Product,EtradeContext>,IProductDAL
    {
    }
}
