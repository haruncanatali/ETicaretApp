using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class Musteri:IEntity
    {
        public Musteri()
        {
            Sepeti = new List<Sepet>();
        }
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tckn { get; set; } // Tc Kimlik numarası
        public string Unvan { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<Sepet> Sepeti { get; set; }
    }
}