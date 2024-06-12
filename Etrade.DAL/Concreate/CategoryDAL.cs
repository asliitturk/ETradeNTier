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
    //Kategori işlemleri için özelleştirilmiş veritabanı erişim sınıfı
    public class CategoryDAL : GenericRepository<Category, EtradeContext>,ICategoryDAL
    {
        //Genel GenericRepository sınıfından türerilmiş ve ICategoryDAL interface'sini uygulayan CategoryDAL sınıfı

        //Herhangi özel bir metot eklenmedi, çünkü GenericRepository sınıfı CRUD işlemlerini zaten içermektedir.
        //Bu sınıf, Category için veritabanı işlemlerini yönetir ve GenericRepository'den gelen genel metotları kullanır.
    }
}
