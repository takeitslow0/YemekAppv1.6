using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace YemekApp.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Seed()
        {
            using (YemekAppContext db = new YemekAppContext())
            {
                // Verilerin silinmesi
                var kullaniciDetayiEntities = db.KullaniciDetaylari.ToList();
                db.KullaniciDetaylari.RemoveRange(kullaniciDetayiEntities);

                var kullaniciEntities = db.Kullanicilar.ToList();
                db.Kullanicilar.RemoveRange(kullaniciEntities);

                var rolEntities = db.Roller.ToList();
                db.Roller.RemoveRange(rolEntities);

                var sehirEntities = db.Sehirler.ToList();
                db.Sehirler.RemoveRange(sehirEntities);

                var ulkeEntities = db.Ulkeler.ToList();
                db.Ulkeler.RemoveRange(ulkeEntities);

                var YemekTarifiEntities = db.YemekTarifleri.ToList();

                db.YemekTarifleri.RemoveRange(YemekTarifiEntities);

                var MalzemeEntities = db.Malzemeler.ToList();
                db.Malzemeler.RemoveRange(MalzemeEntities);

                if (MalzemeEntities.Count > 0)
                {
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppTarifler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppMalzemeler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppKullanicilar', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppSehirler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppUlkeler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('YemekAppRoller', RESEED, 0)");
                }

                //Verilerin eklenmesi

                db.Malzemeler.Add(new Malzeme()
                {
                    Adi = "Muz",
                    //Adi = db.Malzemeler.SingleOrDefault(m => m.Adi == m.Adi).Adi,
                    YemekTarifleri = new List<YemekTarifi>()
                    {
                        new YemekTarifi()
                        {
                            Adi = "Pancake",
                            Tarif = "1. Pankek yapmak için öncelikle yumurtalar ve şekeri uygun bir karıştırma kabına alarak iyice çırpalım.\n 2. Süt, un, kabartma tozu, vanilya ilave çırpma teli ile çırpalım. Siz dilerseniz mikser ile de çırpabilirsiniz.Kek kıvamından biraz daha koyu olacak şekilde hamur hazırlayalım.\n 3. Teflon ya da yapışmaz bir tavaya az sıvı yağı dökelim, fırça yardımı ile her tarafına dağıtalım. Sıvı yağı az kullanmak pankek yapmanın püf noktalarındandır. Dilerseniz peçete ile de tavaya dağıtabilirsiniz.\n 4. 1 büyük kaşık hamur dökülerek hamurun kendi kendine yayılmasını bekleyelim.\n 5. Üzeri göz göz olmaya başlayan pankeklerimizi spatula yardımı ile ters çevirelim. Diğer taraflarını da pişirelim.\n 6. Her iki tarafı da pişen pankeklerimizi servis tabağına alalım.\n 7. Üzerine  pudra şekeri serpilerek zevkinize göre muz, kivi, çilek, çikolata, bal, sürülebilir çikolata, reçel ile servis edebilirsiniz."
                        }
                    }
                });

                db.SaveChanges();

                db.Ulkeler.Add(new Ulke()
                {
                    Adi = "Türkiye",
                    Sehirler = new List<Sehir>()
                    {
                        new Sehir()
                        {
                            Adi = "Ankara"
                        },
                        new Sehir()
                        {
                            Adi = "İstanbul"
                        },
                        new Sehir()
                        {
                            Adi = "İzmir"
                        }
                    }
                });
                db.Ulkeler.Add(new Ulke()
                {
                    Adi = "Amerika Birleşik Devletleri",
                    Sehirler = new List<Sehir>()
                    {
                        new Sehir()
                        {
                            Adi = "New York"
                        },
                        new Sehir()
                        {
                            Adi = "Los Angeles"
                        }
                    }
                });

                db.SaveChanges();

                db.Roller.Add(new Rol()
                {
                    Adi = "Admin",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            Adi="Melik",
                            Soyadi="Baykal",
                            KullaniciAdi = "melik123",
                            Sifre = "melik",
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "melik@yemekapp.com",
                                UlkeId = db.Ulkeler.SingleOrDefault(u => u.Adi == "Türkiye").Id,
                                SehirId = db.Sehirler.SingleOrDefault(s => s.Adi == "Ankara").Id
                            }
                        },
                        new Kullanici()
                        {
                            Adi="Yağız",
                            Soyadi = "yılmaz",
                            KullaniciAdi = "yagiz123",
                            Sifre = "yagiz",
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "yagiz@yemekapp.com",
                                UlkeId = db.Ulkeler.SingleOrDefault(u => u.Adi == "Türkiye").Id,
                                SehirId = db.Sehirler.SingleOrDefault(s => s.Adi == "Ankara").Id
                            }
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Kullanıcı",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            Adi="ahmet",
                            Soyadi="xxa",
                            KullaniciAdi = "ahmet123",
                            Sifre = "ahmet",
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "ahmet@yemekapp.com",
                                UlkeId = db.Ulkeler.SingleOrDefault(u => u.Adi == "Türkiye").Id,
                                SehirId = db.Sehirler.SingleOrDefault(s => s.Adi == "Ankara").Id
                            }
                        }
                    }
                });

                db.SaveChanges();
            }
            return Content("<label style=\"color:red;\"><b>İlk veriler oluşturuldu.</b></label>", "text/html", Encoding.UTF8);
        }
    }
}
