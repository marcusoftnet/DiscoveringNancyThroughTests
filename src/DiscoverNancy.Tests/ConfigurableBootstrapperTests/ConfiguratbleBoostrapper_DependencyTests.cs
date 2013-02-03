using System.Collections.Generic;
using DiscoverNancy.Web.Repositories;
using Nancy;

namespace DiscoverNancy.Tests.ConfigurableBootstrapperTests
{
    public class ConfigBootTestModule : NancyModule
    {
        public ConfigBootTestModule() : base("/config")
        {
            Get["/"] = p => { return "Hello configured fellow"; };
        }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        public IList<object> All()
        {
            return new List<object>(new[]
                {
                        new {Name = "Albert", Age = 5}, 
                        new {Name = "Arvid", Age = 3}, 
                        new {Name = "Gustav", Age = 3}, 
                });
        }
    }
}
