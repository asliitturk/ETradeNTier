using Etrade.Business.Concreate;
using Etrade.DAL.Abstract;
using Etrade.Data.Context;
using Etrade.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.DAL.Concreate
{
    public class OrderDAL : GenericRepository<Order, EtradeContext>,IOrderDAL
    {
    }
}
