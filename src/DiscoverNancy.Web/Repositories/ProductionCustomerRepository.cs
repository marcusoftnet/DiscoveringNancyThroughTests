using System.Collections.Generic;

namespace DiscoverNancy.Web.Repositories
{
    public interface ICustomerRepository 
    {
        IList<dynamic> All();
    }

    public class ProductionCustomerRepository : ICustomerRepository
    {
        public IList<dynamic> All()
        {
            return new List<dynamic>(new[]
                {
                        new {Name = "Prod 1", Age = 40}, 
                        new {Name = "Prod 2", Age = 5}, 
                        new {Name = "Prod 3", Age = 3}, 
                        new {Name = "Prod 4", Age = 3}, 
                });
        }
    }
}