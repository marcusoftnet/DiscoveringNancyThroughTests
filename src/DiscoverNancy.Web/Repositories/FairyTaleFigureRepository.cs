using DiscoverNancy.Web.Models;
using Simple.Data;

namespace DiscoverNancy.Web.Repositories
{
    public interface IFairyTaleFigureRepository
    {
        FairyTaleFigure GetFigureByName(string name);
        void Store(FairyTaleFigure figure);
    }

    public class FairyTaleFigureRepository : IFairyTaleFigureRepository
    {
        private dynamic _db;
        private dynamic DB
        {
            get { return _db ?? (_db = Database.Open()); }
        }


        public FairyTaleFigure GetFigureByName(string name)
        {
            return DB.FairyTaleFigure.FindByName(name);
        }

        public void Store(FairyTaleFigure figure)
        {
            DB.FairyTaleFigure.Insert(figure);
        }
    }
}