using System.Collections.Generic;
using DiscoverNancy.Web.Models;
using DiscoverNancy.Web.Repositories;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace DiscoverNancy.Web
{
    public class FairyTaleFigureModule : NancyModule
    {
        public const string FIGURE_URL = "/figure";
        private readonly IFairyTaleFigureRepository _repository;

        #region "Don't look here"
        /// <summary>
        /// This constructor is just here since other tests that
        /// used DefaultNancyBootstrapper is scanning all the assemblies and
        /// fails on this module that needs a dependency
        /// </summary>
        public FairyTaleFigureModule() { }
        #endregion

        public FairyTaleFigureModule(IFairyTaleFigureRepository repository)
            : base(FIGURE_URL)
        {
            _repository = repository;

            Get["/{name}/View"] = p =>
                                 {
                                     return View["FariyTaleFigure",
                                         _repository.GetFigureByName(p.Name)];
                                 };

            Post["/"] = p =>
                        {
                            var figure = StoreFigure();
                            return new RedirectResponse(UrlForFigure(figure));
                        };
        }

        private static string UrlForFigure(FairyTaleFigure figure)
        {
            return string.Format(FIGURE_URL + "/{0}/View", figure.Name);
        }

        private FairyTaleFigure StoreFigure()
        {
            var figure = this.Bind<FairyTaleFigure>();
            figure.Hangarounds = BindHangarounds(figure); ;

            _repository.Store(figure);
            return figure;
        }

        private List<FairyTaleFigure> BindHangarounds(FairyTaleFigure figure)
        {
            // Yeah - this needs some work ... I know. 
            // Tell me how to better do this. Please. I need help over here
            var hangarounds = new List<FairyTaleFigure>();
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Hangarounds"))
                {
                    var name = Request.Form[key];
                    hangarounds.Add(new FairyTaleFigure { Evil = figure.Evil, Name = name });
                }
            }
            return hangarounds;
        }
    }
}