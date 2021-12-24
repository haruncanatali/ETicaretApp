using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ETicaretApp.Models;

namespace ETicaretApp.Mapping
{
    public class SepetMap:EntityTypeConfiguration<Sepet>
    {
        public SepetMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity);

            this.ToTable("Tbl_Sepet");
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.ToplamFiyat).HasColumnName("ToplamFiyat");
            this.Property(c => c.MusteriId).HasColumnName("MusteriId");
            //this.Property(c => c.AlisSekliId).HasColumnName("AlisSekliId");
        }
    }
}