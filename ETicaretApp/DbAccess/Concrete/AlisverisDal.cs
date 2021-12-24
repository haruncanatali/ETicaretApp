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
    public class AlisverisDal:EntityRepository<Alisveris,ETicaretAppDbContext>,IAlisverisDal
    {
        public Alisveris GetEntity(Expression<Func<Alisveris, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Alisveris.Include(c => c.Urunu).Include(c=>c.Sepeti).FirstOrDefault(filter);
            }
        }

        public List<Alisveris> GetEntities(Expression<Func<Alisveris, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null
                    ? context.Tbl_Alisveris.Include(c => c.Urunu).Include(c=>c.Sepeti).ToList()
                    : context.Tbl_Alisveris.Include(c => c.Sepeti).Include(c=>c.Urunu).Where(filter).ToList();
            }
        }
    }
}