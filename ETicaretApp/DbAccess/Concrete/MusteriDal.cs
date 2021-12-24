using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.Models;

namespace ETicaretApp.DbAccess.Concrete
{
    public class MusteriDal:EntityRepository<Musteri,ETicaretAppDbContext>,IMusteriDal
    {
        public Musteri GetEntity(Expression<Func<Musteri, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Musteri.Include(c=>c.Sepeti).FirstOrDefault(filter);
            }
        }

        public List<Musteri> GetEntities(Expression<Func<Musteri, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null ? context.Tbl_Musteri.Include(c=>c.Sepeti).ToList() : context.Tbl_Musteri.Include(c=>c.Sepeti).Where(filter).ToList();
            }
        }
    }
}