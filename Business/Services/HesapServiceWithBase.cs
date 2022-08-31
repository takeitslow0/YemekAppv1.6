using AppCore.Business.Models.Results;
using Business.Enums;
using Business.Models;

namespace Business.Services
{
    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel model);
        Result Kayit(KullaniciKayitModel model);
    }

    public class HesapService : IHesapService
    {
        private readonly IKullaniciService _kullaniciService;

        public HesapService(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public Result<KullaniciModel> Giris(KullaniciGirisModel model)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k => k.KullaniciAdi == model.KullaniciAdi && k.Sifre == model.Sifre && k.AktifMi);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Geçersiz kullanıcı adı ve şifre!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }

        public Result Kayit(KullaniciKayitModel model)
        {
            var kullanici = new KullaniciModel()
            {
                AktifMi = true,

                RolId = (int)Rol.Kullanıcı,

                Adi = model.Adi,
                Soyadi = model.Soyadi,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                KullaniciDetayi = new KullaniciDetayiModel()
                {
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet,
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    SehirId = model.KullaniciDetayi.SehirId,
                    UlkeId = model.KullaniciDetayi.UlkeId
                }
            };
            return _kullaniciService.Add(kullanici);
        }
    }
}
