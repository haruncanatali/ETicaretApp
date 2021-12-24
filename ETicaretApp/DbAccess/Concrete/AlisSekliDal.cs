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
    public class AlisSekliDal:EntityRepository<AlisSekli,ETicaretAppDbContext>,IAlisSekliDal
    {
        public AlisSekli GetEntity(Expression<Func<AlisSekli, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_AlisSekli.Include(c => c.Sepeti).FirstOrDefault(filter);
            }
        }

        public List<AlisSekli> GetEntities(Expression<Func<AlisSekli, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null
                    ? context.Tbl_AlisSekli.Include(c => c.Sepeti).ToList()
                    : context.Tbl_AlisSekli.Include(c => c.Sepeti).Where(filter).ToList();
            }
        }
    }
}