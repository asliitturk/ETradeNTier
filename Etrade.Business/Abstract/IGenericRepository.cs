using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Business.Abstract
{
    //Genel veritabanı işlemleri için kullanılacak arayüz
    public interface IGenericRepository<Tentity>
        where Tentity : class, new()
    {
        //Belirli filtreleme koşuluyla tüm nesneleri getiren metod
        //Filtre parametresi opsiyoneldir ve varsayılan değeri null'dır
        //Bu, tüm nesne getirmek için kullanılabilir veya filtre belirtilerek 
        //Belirli bir koşulu sağlayan nesne getirmek için kullanılabilir
        List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null);


        //Belirli bir Id'ye sahip nesneyi getiren sorgu
        Tentity Get(int id);


        //Belirli filtreleme koşuluyla nesneyi getiren metod
        //Bu, belirli bir koşulu sağlayan nesneleri getirmek içim kullanılır.
        Tentity Get(Expression<Func<Tentity, bool>> filter);


        //Yeni bir nesne eklemek için kullanılan metod
        void Add(Tentity tentity);

        //Nesneyi güncellem içim kullanılan metod
        void Update(Tentity tentity);

        //Belirli bir Id'ye sahip nesneleri silmek için kullanılan metod
        void Delete(int id);

        //Belirli bir nesneyi silmek için kullanılan metod
        void Delete(Tentity tentity);

    }
}
