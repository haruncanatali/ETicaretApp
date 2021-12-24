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
    public class SepetDal:EntityRepository<Sepet,ETicaretAppDbContext>,ISepetDal
    {
        public Sepet GetEntity(Expression<Func<Sepet, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Sepet.Include(c => c.YapilanAlisverisler).Include(c => c.AlisSekli)
                    .Include(c => c.Indirimi).Include(c => c.Musteri).FirstOrDefault(filter);
            }
        }

        public List<Sepet> GetEntities(Expression<Func<Sepet, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null
                    ? context.Tbl_Sepet.Include(c => c.YapilanAlisverisler).Include(c => c.AlisSekli)
                        .Include(c => c.Indirimi).Include(c => c.Musteri).ToList()
                    : context.Tbl_Sepet.Include(c => c.YapilanAlisverisler).Include(c => c.AlisSekli)
                        .Include(c => c.Indirimi).Include(c => c.Musteri).Where(filter).ToList();
            }
        }
    }
}