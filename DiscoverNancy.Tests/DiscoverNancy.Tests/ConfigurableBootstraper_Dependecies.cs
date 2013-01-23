using System.Collections.Generic;
using DiscoverNancy.Web;
using DiscoverNancy.Web.Repositories;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class ConfigurableBootstraper_Dependecies
    {
        // The FakeCustomerRepository returns 3 customers
        // The FakeOrderRepository returns 4 "orders"

        [Fact]
        public void supplying_two_dependencies_instances()
        {
            // Arrange
            var browser = new Browser(with =>
            {
                with.Module<ModuleWithTwoDependencies>();
                with.Dependencies(
                    new FakeCustomerRepository(),
                    new FakeOrderRepository());
            });

            // Act
            var response = browser.Get("/2dependencies");

            // Assert
            Assert.Equal("Number of customers: 3\nNumber of orders:4", response.Body.AsString());
        }

        [Fact]
        public void supplying_two_dependencies_types()
        {
            // Arrange
            var browser = new Browser(with =>
            {
                with.Module<ModuleWithTwoDependencies>();
                with.Dependencies(
                    typeof(FakeCustomerRepository),
                    typeof(FakeOrderRepository));
            });

            // Act
            var response = browser.Get("/2dependencies");

            // Assert
            Assert.Equal("Number of customers: 3\nNumber of orders:4", response.Body.AsString());
        }


        [Fact]
        public void EnableAutoRegistration()
        {
            // Arrange
            var browser = new Browser(with =>
                {
                    with.Module<ModuleWithTwoDependencies>();
                    with.EnableAutoRegistration();
                });

            // Act
            var response = browser.Get("/2dependencies");

            // Assert
            var bodyString = response.Body.AsString();

            // Since we haven't registred anything for the two dependencies
            // for this module this couldn't work without the EnableAutoRegistreration turned on
            // 
            // Beware though since the order is tricky here. 
            // I suspect that it first scans this Assembly (DiscoverNancy.Tests in our case) 
            // and then referenced assemblies (DiscoverNancy.Web in our case).
            // Resulting in our FakeAssemblies being read in. 

            // As the result shows us
            Assert.Equal("Number of customers: 3\nNumber of orders:4", bodyString);
        }
    }



    public class FakeOrderRepository : IOrderRepository
    {
        public IList<dynamic> All()
        {
            return new List<dynamic> { 1, 2, 3, 4 };
        }
    }
}
