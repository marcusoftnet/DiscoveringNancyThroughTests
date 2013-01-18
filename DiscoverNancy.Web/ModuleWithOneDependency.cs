using DiscoverNancy.Web.Repositories;
using Nancy;

namespace DiscoverNancy.Web
{
    public class ModuleWithOneDependency : NancyModule
    {
        public ModuleWithOneDependency(ICustomerRepository repository) : base("/dependency")
        {
            Get["/"] = _ =>
                           {
                               return string.Format("Number of customers: {0}", 
                                   repository.All().Count);
                           };
        }
    }
}