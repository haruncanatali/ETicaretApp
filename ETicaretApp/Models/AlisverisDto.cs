using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class AlisverisDto
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int UrunId { get; set; }
        public int Miktar { get; set; }
    }
}