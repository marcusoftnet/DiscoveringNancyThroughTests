using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiscoverNancy.Web.Repositories;
using Nancy;

namespace DiscoverNancy.Web
{
    public class ModuleWithTwoDependencies : NancyModule
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public ModuleWithTwoDependencies(
            ICustomerRepository customerRepository, 
            IOrderRepository orderRepository) : base("/2dependencies")
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;

            Get["/"] = p => { return OrdersAndCustomers(); };
        }

        private string OrdersAndCustomers()
        {
            return string.Format("Number of customers: {0}\nNumber of orders:{1}", 
                _customerRepository.All().Count,
                _orderRepository.All().Count);
        }
    }

    
}