using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class YemekAppContext : DbContext
    {
        public DbSet<YemekTarifi> YemekTarifleri { get; set; }
        public DbSet<Malzeme> Malzemeler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KullaniciDetayi> KullaniciDetaylari { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"server=.\SQLEXPRESS01;database=YemekApp;trusted_connection=true;multipleactiveresultsets=true;";


            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YemekTarifi>()
                .ToTable("YemekAppTarifler")
                .HasOne(YemekTarifi => YemekTarifi.Malzemeler)
                .WithMany(Malzemeler => Malzemeler.YemekTarifleri)
                .HasForeignKey(YemekTarifi => YemekTarifi.MalzemeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Malzeme>()
                .ToTable("YemekAppMalzemeler");

            modelBuilder.Entity<Kullanici>()
                .ToTable("YemekAppKullanicilar")
                .HasOne(kullanici => kullanici.Rol)
                .WithMany(rol => rol.Kullanicilar)
                .HasForeignKey(kullanici => kullanici.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .ToTable("YemekAppKullaniciDetaylari")
                .HasOne(kullaniciDetayi => kullaniciDetayi.Kullanici)
                .WithOne(kullanici => kullanici.KullaniciDetayi)
                .HasForeignKey<KullaniciDetayi>(kullaniciDetayi => kullaniciDetayi.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Ulke)
                .WithMany(ulke => ulke.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Sehir)
                .WithMany(sehir => sehir.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.SehirId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sehir>()
                .ToTable("YemekAppSehirler")
                .HasOne(sehir => sehir.Ulke)
                .WithMany(ulke => ulke.Sehirler)
                .HasForeignKey(sehir => sehir.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ulke>().ToTable("YemekAppUlkeler");

            modelBuilder.Entity<Rol>().ToTable("YemekAppRoller");

            modelBuilder.Entity<YemekTarifi>().HasIndex(yemekTarifi => yemekTarifi.Adi);

            modelBuilder.Entity<Kullanici>().HasIndex(kullanici => kullanici.KullaniciAdi).IsUnique();

            modelBuilder.Entity<KullaniciDetayi>().HasIndex(kullaniciDetay => kullaniciDetay.Eposta).IsUnique();
        }
    }
}
