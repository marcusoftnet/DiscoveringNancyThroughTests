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

        public FairyTaleFigureRepository()
        {
            _db = Database.Open();
        }

        public FairyTaleFigure GetFigureByName(string name)
        {
            return _db.FairyTaleFigure.FindByName(name);
        }

        public void Store(FairyTaleFigure figure)
        {
            _db.FairyTaleFigure.Insert(figure);
        }
    }
}