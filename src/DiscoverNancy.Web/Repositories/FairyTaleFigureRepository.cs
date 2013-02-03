using DiscoverNancy.Web.Models;
using Simple.Data;

namespace DiscoverNancy.Web.Repositories
{
    public interface IFairyTaleFigureRepository
    {
        FairyTaleFigure GetFigureByName(string name);
    }
    
    public class FairyTaleFigureRepository : IFairyTaleFigureRepository
    {
        public FairyTaleFigure GetFigureByName(string name)
        {
            var db = Database.Open();
            return db.FairyTaleFigure.FindByName(name);
        }
    }
}