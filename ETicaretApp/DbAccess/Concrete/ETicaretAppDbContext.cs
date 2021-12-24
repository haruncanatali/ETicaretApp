using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ETicaretApp.Mapping;
using ETicaretApp.Models;

namespace ETicaretApp.DbAccess.Concrete
{
    public class ETicaretAppDbContext:DbContext
    {
        public ETicaretAppDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AlisSekli> Tbl_AlisSekli { get; set; }
        public DbSet<Alisveris> Tbl_Alisveris { get; set; }
        public DbSet<Indirim> Tbl_Indirim { get; set; }
        public DbSet<Kategori> Tbl_Kategori { get; set; }
        public DbSet<Marka> Tbl_Marka { get; set; }
        public DbSet<Sepet> Tbl_Sepet { get; set; }
        public DbSet<Musteri> Tbl_Musteri { get; set; }
        public DbSet<Urun> Tbl_Urun { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Relationships
            //->AlisSekli
            modelBuilder.Entity<Sepet>().HasMany(c => c.AlisSekli).WithRequired(c => c.Sepeti)
                .HasForeignKey(c => c.SepetId);
            //->Alisveris
            modelBuilder.Entity<Urun>().HasMany(c => c.Alisverisleri).WithRequired(c => c.Urunu)
                .HasForeignKey(c => c.UrunId);
            
            modelBuilder.Entity<Sepet>().HasMany(c => c.YapilanAlisverisler).WithRequired(c => c.Sepeti)
                .HasForeignKey(c => c.SepetId);
            //->Indirim
            modelBuilder.Entity<Sepet>().HasMany(c => c.Indirimi).WithRequired(c => c.Sepeti)
                .HasForeignKey(c => c.SepetId);
            //->Kategori
            modelBuilder.Entity<Kategori>().HasMany(c => c.Urunleri).WithRequired(c => c.Kategorisi)
                .HasForeignKey(c => c.KategoriId);
            //->Marka
            modelBuilder.Entity<Marka>().HasMany(c => c.Urunleri).WithRequired(c => c.Markasi)
                .HasForeignKey(c => c.MarkaId);
            //->Musteri
            modelBuilder.Entity<Musteri>().HasMany(c => c.Sepeti).WithRequired(c => c.Musteri)
                .HasForeignKey(c => c.MusteriId);

            //Mapping
            modelBuilder.Configurations.Add(new AlisSekliMap());
            modelBuilder.Configurations.Add(new AlisVerisMap());
            modelBuilder.Configurations.Add(new IndirimMap());
            modelBuilder.Configurations.Add(new KategoriMap());
            modelBuilder.Configurations.Add(new MarkaMap());
            modelBuilder.Configurations.Add(new MusteriMap());
            modelBuilder.Configurations.Add(new SepetMap());
            modelBuilder.Configurations.Add(new UrunMap());
        }
    }
}