using Etrade.Business.Abstract;
using Etrade.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.DAL.Abstract
{
    //Kategori veritabanı işlemleri için interface
    public interface ICategoryDAL : IGenericRepository<Category>
    {
        //IGenericRepository interface'i miras alır, böylece genel CRUD işlemlerini içerir.

        //Ek özel metotlar veya işlemler burada tanımlanabilir, ancak genel CRUD işlemleri IGenericRepository'den gelir.
    }
}
