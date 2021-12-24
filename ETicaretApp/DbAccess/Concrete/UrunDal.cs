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
    public class UrunDal:EntityRepository<Urun,ETicaretAppDbContext>,IUrunDal
    {
        public Urun GetEntity(Expression<Func<Urun, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Urun.Include(c => c.Kategorisi).Include(c => c.Markasi).FirstOrDefault(filter);
            }
        }

        public List<Urun> GetEntities(Expression<Func<Urun, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null
                    ? context.Tbl_Urun.Include(c => c.Kategorisi).Include(c => c.Markasi).ToList()
                    : context.Tbl_Urun.Include(c => c.Kategorisi).Include(c => c.Markasi).Where(filter).ToList();
            }
        }
    }
}