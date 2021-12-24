using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class Urun:IEntity
    {
        public Urun()
        {
            Alisverisleri = new List<Alisveris>();
        }

        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string Bedeni { get; set; }
        public decimal Fiyati { get; set; }

        public int KategoriId { get; set; }
        public int MarkaId { get; set; }

        public virtual ICollection<Alisveris> Alisverisleri { get; set; }
        public virtual Kategori Kategorisi { get; set; }
        public virtual Marka Markasi { get; set; }

    }
}