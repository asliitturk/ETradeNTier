using Etrade.Business.Abstract;
using Etrade.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.DAL.Abstract
{
    public interface IOrderDAL:IGenericRepository<Order>
    {
    }
}
