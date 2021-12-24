using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class Alisveris:IEntity
    {
        public int Id { get; set; }
        public int Miktar { get; set; }

        public int UrunId { get; set; }
        public int SepetId { get; set; }

        public virtual Urun Urunu { get; set; }
        public virtual Sepet Sepeti { get; set; }
    }
}