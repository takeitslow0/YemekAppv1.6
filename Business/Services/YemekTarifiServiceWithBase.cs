using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using Business.Models.Filters;
using Business.Models.Raports;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IYemekTarifiService : IService<YemekTarifiModel, YemekTarifi, YemekAppContext>
    {
        Task<Result<List<YemekTarifiRaporModel>>> RaporGetirAsync(YemekTarifiRaporFilterModel filtre, PageModel sayfa, OrderModel sira);
    }

    public class YemekTarifiService : IYemekTarifiService
    {
        public RepoBase<YemekTarifi, YemekAppContext> Repo { get; set; } = new Repo<YemekTarifi, YemekAppContext>();

        private readonly RepoBase<Malzeme, YemekAppContext> _MalzemeRepo;

        public YemekTarifiService()
        {
            YemekAppContext yemekAppContext = new YemekAppContext();
            Repo = new Repo<YemekTarifi, YemekAppContext>(yemekAppContext);

            _MalzemeRepo = new Repo<Malzeme, YemekAppContext>(yemekAppContext);
        }

        public IQueryable<YemekTarifiModel> Query()
        {
            return Repo.Query("Malzeme").OrderBy(yt => yt.Malzemeler.Adi).ThenBy(yt => yt.Adi).Select(yt => new YemekTarifiModel() // Bunu yazdım da bende çok anlamadım
            {
                Id = yt.Id,
                Adi = yt.Adi,
                Tarif = yt.Tarif,
                MalzemeId = yt.MalzemeId,

                // ALTTAKİNİ E-TİCARET PROJESİNDEN ALDIM GÜZEL BİLGİ BELKİ KULLANIRIZ DİYE

                // eğer ürün model üzerinden bir kategorinin adı dışında diğer özellikleri (Id, Aciklamasi, vb.) de kullanılmak isteniyorsa bu şekilde modelde referans tanımlanabilir ve bu referans new'lenerek set edilebilir.
                // ürün model üzerinden KategoriDisplay kullanımı (genelde view'de): urunModel.KategoriDisplay.Id, urunModel.KategoriDisplay.Adi, urunModel.KategoriDisplay.Aciklamasi
                //KategoriDisplay = new KategoriModel()
                //{
                //    Id = u.Kategori.Id,
                //    Adi = u.Kategori.Adi,
                //    Aciklamasi = u.Kategori.Aciklamasi
                //},
                MalzemeAdiDisplay = yt.Malzemeler.Adi,
            });
        }

        public Result Add(YemekTarifiModel model)
        {
            if (Repo.Query().Any(yt => yt.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz ürün adına sahip kayıt bulunmaktadır!");
            YemekTarifi entity = new YemekTarifi()
            {
                Adi = model.Adi.Trim(),
                Tarif = model.Tarif.Trim(),
                MalzemeId = model.MalzemeId.Value,
            };
            Repo.Add(entity);

            model.Id = entity.Id;

            return new SuccessResult("Yemek tarifi başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            YemekTarifi yemekTarifi = Repo.Query(yt => yt.Id == id).SingleOrDefault();

            Repo.Delete(u => u.Id == id);

            return new SuccessResult("Yemek Tarifi başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public async Task<Result<List<YemekTarifiRaporModel>>> RaporGetirAsync(YemekTarifiRaporFilterModel filtre, PageModel sayfa, OrderModel sira)
        {
            List<YemekTarifiRaporModel> list;

            #region Select Sorgusu
            var yemekTarifiQuery = Repo.Query();
            var malzemeQuery = _MalzemeRepo.Query();

            var query = from yemekTarifi in yemekTarifiQuery
                        join malzeme in malzemeQuery
                        on yemekTarifi.MalzemeId equals malzeme.Id into malzemeler
                        from subMalzemeler in malzemeler.DefaultIfEmpty()
                        select new YemekTarifiRaporModel()
                        {
                            MalzemeAdi = subMalzemeler.Adi,
                            MalzemeId = subMalzemeler.Id,
                            YemekTarifi = yemekTarifi.Tarif,
                            YemekAdi = yemekTarifi.Adi,
                        };
            #endregion

            #region Sıra
            switch (sira.Expression)
            {
                case "Malzeme":
                    query = sira.DirectionAscending
                        ? query.OrderBy(q => q.MalzemeAdi)
                        : query.OrderByDescending(q => q.MalzemeAdi);
                    break;
                case "Yemek":
                    query = sira.DirectionAscending
                        ? query.OrderBy(q => q.YemekAdi)
                        : query.OrderByDescending(q => q.YemekAdi);
                    break;
            }
            #endregion

            #region Filtre
            if (filtre.MalzemeId.HasValue)
                query = query.Where(q => q.MalzemeId == filtre.MalzemeId.Value);
            if (!string.IsNullOrWhiteSpace(filtre.YemekAdi))
                query = query.Where(q => q.YemekAdi.ToLower().Contains(filtre.YemekAdi.ToLower().Trim()));
            #endregion

            #region Sayfa
            sayfa.RecordsCount = query.Count();
            int skip = (sayfa.PageNumber - 1) * sayfa.RecordsPerPageCount;
            int take = sayfa.RecordsPerPageCount;
            query = query.Skip(skip).Take(take);
            #endregion

            list = await query.ToListAsync();
            if (list.Count == 0)
                return new ErrorResult<List<YemekTarifiRaporModel>>("Kayıt bulunamadı!");
            return new SuccessResult<List<YemekTarifiRaporModel>>(sayfa.RecordsCount + " kayıt bulundu.", list);
        }

        public Result Update(YemekTarifiModel model)
        {
            if (Repo.Query().Any(yt => yt.Adi.ToUpper() == model.Adi.ToUpper().Trim() && yt.Id != model.Id))
                return new ErrorResult("Girdiğiniz yemek adına sahip kayıt bulunmaktadır!");

            YemekTarifi entity = Repo.Query(yt => yt.Id == model.Id, "yemekTarifi").SingleOrDefault();

            entity.Adi = model.Adi.Trim();
            entity.Tarif = model.Tarif.Trim();
            entity.MalzemeId = model.MalzemeId.Value;

            Repo.Update(entity);
            return new SuccessResult("Yemek başarıyla güncellendi.");
        }
    }
}
