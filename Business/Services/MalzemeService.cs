using AppCore.Business.Models.Results;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class MalzemeService : IMalzemeService
    {
        public RepoBase<Malzeme, YemekAppContext> Repo { get; set; } = new Repo<Malzeme, YemekAppContext>();

        public IQueryable<MalzemeModel> Query()
        {
            IQueryable<MalzemeModel> query = Repo.Query("YemekTarifleri").OrderBy(malzeme => malzeme.Adi).Select(malzeme => new MalzemeModel()
            {
                Id = malzeme.Id,
                Adi = malzeme.Adi,
            });
            return query;
        }

        public Result Add(MalzemeModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Adi))
                return new ErrorResult("Malzeme adı boş olamaz!");
            if (model.Adi.Length > 100)
                return new ErrorResult("Malzeme adı en fazla 100 karakter olmalıdır!");

            if (Repo.Query().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz malzeme adına sahip kayıt bulunmaktadır!");

            Malzeme newEntity = new Malzeme()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Add(newEntity);

            return new SuccessResult();
        }

        public Result Update(MalzemeModel model)
        {
            if (Repo.Query().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim() && m.Id != model.Id))
                return new ErrorResult("Girdiğiniz malzeme adına sahip kayıt bulunmaktadır!");
            Malzeme entity = Repo.Query(m => m.Id == model.Id).SingleOrDefault();
            if (entity == null)
                return new ErrorResult("Malzeme kaydı bulunamadı!");
            entity.Adi = model.Adi.Trim();
            Repo.Update(entity);
            return new SuccessResult("Malzeme başarıyla güncellendi.");
        }

        public Result Delete(int id)
        {
            Malzeme entity = Repo.Query(m => m.Id == id, "YemekTarifleri").SingleOrDefault();
            if (entity.YemekTarifleri != null && entity.YemekTarifleri.Count > 0) // bu id'ye sahip kategorinin ürünleri varsa
            {
                return new ErrorResult("Silinmek istenen malzemeye ait yemek tarifleri bulunmaktadır!");
            }
            Repo.Delete(m => m.Id == id);
            return new SuccessResult("malzeme başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public async Task<List<MalzemeModel>> MalzemeleriGetirAsync()
        {
            List<MalzemeModel> malzemeler;

            malzemeler = await Query().ToListAsync();

            return malzemeler;
        }
    }
}