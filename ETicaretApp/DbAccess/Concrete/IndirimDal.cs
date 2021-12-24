using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.Models;

namespace ETicaretApp.DbAccess.Concrete
{
    public class IndirimDal:EntityRepository<Indirim,ETicaretAppDbContext>,IIndirimDal
    {
        public Indirim GetEntity(Expression<Func<Indirim, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Indirim.FirstOrDefault(filter);
            }
        }

        public List<Indirim> GetEntities(Expression<Func<Indirim, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null ? context.Tbl_Indirim.ToList() : context.Tbl_Indirim.Where(filter).ToList();
            }
        }
    }
}