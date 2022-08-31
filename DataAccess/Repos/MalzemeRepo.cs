using DataAccess.Contexts;
using DataAccess.Repos.Bases;

namespace DataAccess.Repos
{
    public class MalzemeRepo : MalzemeRepoBase
    {
        public MalzemeRepo() : base()
        {

        }

        public MalzemeRepo(YemekAppContext yemekAppContext) : base(yemekAppContext)
        {

        }
    }
}
