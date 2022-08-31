using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repos.Bases
{
    public class MalzemeRepoBase : RepoBase<Malzeme, YemekAppContext>
    {
        protected MalzemeRepoBase() : base()
        {

        }

        protected MalzemeRepoBase(YemekAppContext yemekAppContext) : base(yemekAppContext)
        {

        }
    }
}
