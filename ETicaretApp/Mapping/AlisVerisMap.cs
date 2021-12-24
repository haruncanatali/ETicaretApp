using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ETicaretApp.Models;

namespace ETicaretApp.Mapping
{
    public class AlisVerisMap:EntityTypeConfiguration<Alisveris>
    {
        public AlisVerisMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity);

            this.ToTable("Tbl_Alisveris");
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Miktar).HasColumnName("Miktar");
            this.Property(c => c.UrunId).HasColumnName("UrunId");
            this.Property(c => c.SepetId).HasColumnName("SepetId");
        }
    }
}