using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.Models;

namespace ETicaretApp.DbAccess.Concrete
{
    public class KategoriDal:EntityRepository<Kategori,ETicaretAppDbContext>,IKategoriDal
    {
        public Kategori GetEntity(Expression<Func<Kategori, bool>> filter)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return context.Tbl_Kategori.FirstOrDefault(filter);
            }
        }

        public List<Kategori> GetEntities(Expression<Func<Kategori, bool>> filter = null)
        {
            using (ETicaretAppDbContext context = new ETicaretAppDbContext())
            {
                return filter == null ? context.Tbl_Kategori.ToList() : context.Tbl_Kategori.Where(filter).ToList();
            }
        }
    }
}