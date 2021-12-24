using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class Sepet:IEntity
    {
        public Sepet()
        {
            YapilanAlisverisler = new List<Alisveris>();
            AlisSekli = new List<AlisSekli>();
            Indirimi = new List<Indirim>();
        }

        public int Id { get; set; }
        public int ToplamFiyat { get; set; }
        public int MusteriId { get; set; }

        public virtual Musteri Musteri { get; set; }
        public virtual ICollection<AlisSekli> AlisSekli { get; set; }
        public virtual ICollection<Indirim> Indirimi { get; set; }
        public virtual ICollection<Alisveris> YapilanAlisverisler { get; set; }
    }
}