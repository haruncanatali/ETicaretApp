using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class Kategori:IEntity
    {
        public Kategori()
        {
            Urunleri = new List<Urun>();
        }

        public int Id { get; set; }
        public string KategoriAdi { get; set; }

        public virtual ICollection<Urun> Urunleri { get; set; }
    }
}