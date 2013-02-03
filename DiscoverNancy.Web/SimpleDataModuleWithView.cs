using DiscoverNancy.Web.Models;
using DiscoverNancy.Web.Repositories;
using Nancy;
using Simple.Data;

namespace DiscoverNancy.Web
{
    public class SimpleDataModuleWithView : NancyModule
    {
        private readonly IFairyTaleFigureRepository _repository;

        public SimpleDataModuleWithView(IFairyTaleFigureRepository repository)
        {
            _repository = repository;

            Get["/figure/{name}/View"] = p =>
                                             {
                                                 return View["FariyTaleFigure", 
                                                     _repository.GetFigureByName(p.Name)];
                                             };
        }
    }
}