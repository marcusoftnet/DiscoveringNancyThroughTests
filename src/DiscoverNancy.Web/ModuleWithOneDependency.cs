using DiscoverNancy.Web.Repositories;
using Nancy;

namespace DiscoverNancy.Web
{
    public class ModuleWithOneDependency : NancyModule
    {
        public ModuleWithOneDependency(ICustomerRepository repository)
            : base("/dependency")
        {
            Get["/"] = _ =>
                           {
                               return string.Format("Number of customers: {0}",
                                   repository.All().Count);
                           };
        }


        #region "Don't look here"
        /// <summary>
        /// This constructor is just here since other tests that
        /// used DefaultNancyBootstrapper is scanning all the assemblies and
        /// fails on this module that needs a dependency
        /// </summary>
        public ModuleWithOneDependency() { }
        #endregion
    }
}