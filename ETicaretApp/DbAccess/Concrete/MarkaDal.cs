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
    public class MarkaDal:EntityRepository<Marka,ETicaretAppDbContext>,IMarkaDal
    {
        public Marka GetEntity(Expression<Func<Marka, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Marka.Include(c=>c.Urunleri).FirstOrDefault(filter);
            }
        }

        public List<Marka> GetEntities(Expression<Func<Marka, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null ? context.Tbl_Marka.Include(c=>c.Urunleri).ToList() : context.Tbl_Marka.Include(c=>c.Urunleri).Where(filter).ToList();
            }
        }
    }
}