using System.Collections.Generic;

namespace DiscoverNancy.Web.Repositories
{
    public interface IOrderRepository 
    {
        IList<dynamic> All();
    }

    public class ProductionOrderRepository : IOrderRepository
    {
        public IList<dynamic> All()
        {
            return new List<dynamic>(new[]
                {
                        new {OrderId = 12345, TotalValue = 40000}, 
                        new {OrderId = 23456, TotalValue = 50000}, 
                        new {OrderId = 34567, TotalValue = 60000}, 
                });
        }
    }
}