using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, YemekAppContext>
    {
        Result<List<KullaniciModel>> KullanicilariGetir();
        Result<KullaniciModel> KullaniciGetir(int id);
    }

    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, YemekAppContext> Repo { get; set; } = new Repo<Kullanici, YemekAppContext>();

        private readonly RepoBase<KullaniciDetayi, YemekAppContext> _kullaniciDetayiRepo;
        private readonly RepoBase<Rol, YemekAppContext> _rolRepo;
        private readonly RepoBase<Ulke, YemekAppContext> _ulkeRepo;
        private readonly RepoBase<Sehir, YemekAppContext> _sehirRepo;

        public KullaniciService()
        {
            YemekAppContext yemekAppContext = new YemekAppContext();
            Repo = new Repo<Kullanici, YemekAppContext>(yemekAppContext);
            _kullaniciDetayiRepo = new Repo<KullaniciDetayi, YemekAppContext>(yemekAppContext);
            _rolRepo = new Repo<Rol, YemekAppContext>(yemekAppContext);
            _ulkeRepo = new Repo<Ulke, YemekAppContext>(yemekAppContext);
            _sehirRepo = new Repo<Sehir, YemekAppContext>(yemekAppContext);
        }

        public IQueryable<KullaniciModel> Query()
        {
            var kullaniciQuery = Repo.Query();
            var kullaniciDetayiQuery = _kullaniciDetayiRepo.Query();
            var rolQuery = _rolRepo.Query();
            var ulkeQuery = _ulkeRepo.Query();
            var sehirQuery = _sehirRepo.Query();

            //inner join
            var query = from kullanici in kullaniciQuery
                        join kullaniciDetayi in kullaniciDetayiQuery
                        on kullanici.Id equals kullaniciDetayi.KullaniciId
                        join rol in rolQuery
                        on kullanici.RolId equals rol.Id
                        join ulke in ulkeQuery
                        on kullaniciDetayi.UlkeId equals ulke.Id
                        join sehir in sehirQuery
                        on kullaniciDetayi.SehirId equals sehir.Id
                        orderby rol.Adi, kullanici.KullaniciAdi
                        select new KullaniciModel()
                        {
                            Id = kullanici.Id,
                            Adi = kullanici.Adi,
                            Soyadi = kullanici.Soyadi,
                            KullaniciAdi = kullanici.KullaniciAdi,
                            Sifre = kullanici.Sifre,
                            AktifMi = kullanici.AktifMi,
                            KullaniciDetayi = new KullaniciDetayiModel()
                            {
                                Cinsiyet = kullaniciDetayi.Cinsiyet,
                                Eposta = kullaniciDetayi.Eposta,
                                UlkeId = kullaniciDetayi.UlkeId,
                                UlkeAdiDisplay = ulke.Adi,
                                SehirId = kullaniciDetayi.SehirId,
                                SehirAdiDisplay = sehir.Adi,
                            },
                            RolId = kullanici.RolId,
                            RolAdiDisplay = rol.Adi,
                            AktifDisplay = kullanici.AktifMi ? "Evet" : "Hayır"
                        };
            return query;

        }

        public Result Add(KullaniciModel model)
        {
            // AD VE SOYAD BAŞKA KULLANICININ ADI VE SOYADI İLE AYNI OLABİLİR Mİ DAHA DÜŞÜNMEDİM O YÜZDEN ŞİMDİLİK OLABİLİR AMA KULLANICI ADI AYNI OLAMAZ
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim()))
                return new ErrorResult("Girilen kullanıcı adına sahip kullanıcı kaydı bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim()))
                return new ErrorResult("Girilen e-postaya sahip kullanıcı kaydı bulunmaktadır!");
            var entity = new Kullanici()
            {
                AktifMi = model.AktifMi,
                Adi = model.Adi,
                Soyadi = model.Soyadi,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                RolId = model.RolId.Value,
                KullaniciDetayi = new KullaniciDetayi()
                {
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet,
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    SehirId = model.KullaniciDetayi.SehirId.Value,
                    UlkeId = model.KullaniciDetayi.UlkeId.Value
                }
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            return new SuccessResult("Delete");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public Result<KullaniciModel> KullaniciGetir(int id)
        {
            var kullanici = Query().SingleOrDefault(k => k.Id == id);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Kullanıcı bulunamadı!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }

        public Result<List<KullaniciModel>> KullanicilariGetir()
        {
            var kullanicilar = Query().ToList();
            if (kullanicilar.Count == 0)
                return new ErrorResult<List<KullaniciModel>>("Kullanıcı bulunamadı!");
            return new SuccessResult<List<KullaniciModel>>(kullanicilar.Count + " kullanıcı bulundu.", kullanicilar);
        }

        public Result Update(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen kullanıcı adına sahip kullanıcı kaydı bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen e-postaya sahip kullanıcı kaydı bulunmaktadır!");
            var entity = Repo.Query(k => k.Id == model.Id, "KullaniciDetayi").SingleOrDefault();
            entity.AktifMi = model.AktifMi;
            entity.Adi = model.Adi;
            entity.Soyadi = model.Soyadi;
            entity.KullaniciAdi = model.KullaniciAdi;
            entity.Sifre = model.Sifre;
            entity.RolId = model.RolId.Value;
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Eposta = model.KullaniciDetayi.Eposta.Trim();
            entity.KullaniciDetayi.SehirId = model.KullaniciDetayi.SehirId.Value;
            entity.KullaniciDetayi.UlkeId = model.KullaniciDetayi.UlkeId.Value;
            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
