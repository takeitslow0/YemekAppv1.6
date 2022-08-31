using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services.Bases
{
    public interface IMalzemeService : IService<MalzemeModel, Malzeme, YemekAppContext>
    {
        Task<List<MalzemeModel>> MalzemeleriGetirAsync();
    }
}
