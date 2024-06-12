using Etrade.Business.Abstract;
using Etrade.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Etrade.Business.Concreate
{
    //Genel veritabanı işlemleri gerçekleştirmek üzere tasarlanmış genel repository sınıfı
    public class GenericRepository<Tentity, Tcontext> : IGenericRepository<Tentity>
        where Tentity : class, new()
        where Tcontext : IdentityDbContext<AppUser, AppRole, int>, new()
    {
        //Yeni nesneler eklemek için kullanılan metod
        public void Add(Tentity tentity)
        {
            using (var db = new Tcontext())
            {
                db.Set<Tentity>().Add(tentity);
                db.SaveChanges();
            }
        }
        //Belirli bir Id'ye sahip nesneyi silmek için kullanılan metod
        public void Delete(int id)
        {
            using (var db = new Tcontext())
            {
                var entity = db.Set<Tentity>().Find(id);
                db.Set<Tentity>().Remove(entity);
                db.SaveChanges();
            }
        }

        //Belirli bir nesneyi silmek için kullanılan metod
        public void Delete(Tentity tentity)
        {
            using (var db = new Tcontext())
            {
                db.Entry(tentity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        //Belirtilen Id'ye sahip nesneyi getirir
        public Tentity Get(int id)
        {
            using (var db = new Tcontext())
            {
                var entity = db.Set<Tentity>().Find(id);
                return entity;
            }
        }
        //Belirtilen filtre koşuluna uyan nesneyi getirir
        public Tentity Get(Expression<Func<Tentity, bool>> filter)
        {
            using (var db = new Tcontext())
            {
                var entity = db.Set<Tentity>().Find(filter);
                return entity;
            }
        }
        //Belirtilen filtre koşulun uyan tüm nesneleri getirir.Filtre belirtilmezse, tüm nesneler getirir
        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null)
        {
            using (var db = new Tcontext())
            {
                return filter == null ? db.Set<Tentity>().ToList() : db.Set<Tentity>().Where(filter).ToList();
            }
        }
        //Nesneyi günceller ve değişkenleri kaydeder
        public void Update(Tentity tentity)
        {
            using (var db = new Tcontext())
            {
                db.Entry(tentity).State = EntityState.Modified; 
                db.SaveChanges();
            }
        }
    }
}
