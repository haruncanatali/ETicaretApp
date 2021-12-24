using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETicaretApp.Models
{
    public class AlisSekli:IEntity
    {
        public int Id { get; set; }
        public string AlisTipi { get; set; }

        public int SepetId { get; set; }

        public virtual Sepet Sepeti { get; set; }
    }
}