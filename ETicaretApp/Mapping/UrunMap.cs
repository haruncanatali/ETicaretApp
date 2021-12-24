using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ETicaretApp.Models;

namespace ETicaretApp.Mapping
{
    public class UrunMap:EntityTypeConfiguration<Urun>
    {
        public UrunMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity);

            this.ToTable("Tbl_Urun");
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.UrunAdi).HasColumnName("UrunAdi");
            this.Property(c => c.Fiyati).HasColumnName("Fiyati");
            this.Property(c => c.Bedeni).HasColumnName("Bedeni");
            this.Property(c => c.KategoriId).HasColumnName("KategoriId");
            this.Property(c => c.MarkaId).HasColumnName("MarkaId");
        }
    }
}