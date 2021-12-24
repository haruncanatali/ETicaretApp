using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ETicaretApp.Models;

namespace ETicaretApp.Mapping
{
    public class KategoriMap:EntityTypeConfiguration<Kategori>
    {
        public KategoriMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity);

            this.ToTable("Tbl_Kategori");
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.KategoriAdi).HasColumnName("KategoriAdi");
        }
    }
}