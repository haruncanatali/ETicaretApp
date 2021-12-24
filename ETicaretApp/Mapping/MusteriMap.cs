using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ETicaretApp.Models;

namespace ETicaretApp.Mapping
{
    public class MusteriMap:EntityTypeConfiguration<Musteri>
    {
        public MusteriMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                    .Identity);

            this.ToTable("Tbl_Musteri");
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Ad).HasColumnName("Ad");
            this.Property(c => c.Soyad).HasColumnName("Soyad");
            this.Property(c => c.Tckn).HasColumnName("Tckn");
            this.Property(c => c.Unvan).HasColumnName("Unvan");
            this.Property(c => c.Mail).HasColumnName("Mail");
            this.Property(c => c.Telefon).HasColumnName("Telefon");
        }
    }
}