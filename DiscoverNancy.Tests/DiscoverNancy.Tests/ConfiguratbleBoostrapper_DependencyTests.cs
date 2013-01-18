using System.Collections.Generic;
using DiscoverNancy.Web;
using DiscoverNancy.Web.Repositories;
using Nancy;

namespace DiscoverNancy.Tests
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
        public IList<dynamic> All()
        {
            return new List<dynamic>(new[]
                {
                        new {Name = "Albert", Age = 5}, 
                        new {Name = "Arvid", Age = 3}, 
                        new {Name = "Gustav", Age = 3}, 
                });
        }
    }
}
