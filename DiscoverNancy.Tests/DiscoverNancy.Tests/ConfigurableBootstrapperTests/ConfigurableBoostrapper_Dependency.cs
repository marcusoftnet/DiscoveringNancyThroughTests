using DiscoverNancy.Web;
using DiscoverNancy.Web.Repositories;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.ConfigurableBootstrapperTests
{
    public class ConfigurableBoostrapper_Dependency
    {
        private const string ROOT_URL = "/dependency";

        [Fact]
        public void mocking_dependencies_interface_and_instance()
        {
            // Arrange
            var fakeCustomerRepository = new FakeCustomerRepository();
            var browser = new Browser(cfg =>
                                          {
                                              cfg.Module<ModuleWithOneDependency>();
                                              cfg.Dependency<ICustomerRepository>(fakeCustomerRepository);
                                          });

            // Act
            var response = browser.Get("/dependency");

            // Assert
            Assert.Equal("Number of customers: 3", response.Body.AsString());
        }

        [Fact]
        public void mocking_dependencies_via_type_parameter()
        {
            // Arrange
            var browser = new Browser(cfg =>
                                          {
                                              cfg.Module<ModuleWithOneDependency>();
                                              cfg.Dependency<FakeCustomerRepository>();
                                          });

            // Act
            var response = browser.Get(ROOT_URL);

            // Assert
            Assert.Equal("Number of customers: 3", response.Body.AsString());
        }

        [Fact]
        public void mocking_dependencies_instance_only()
        {
            // Arrange
            var browser = new Browser(cfg =>
                                          {
                                              cfg.Module<ModuleWithOneDependency>();
                                              cfg.Dependency(new FakeCustomerRepository());
                                          });

            // Act
            var response = browser.Get(ROOT_URL);

            // Assert
            Assert.Equal("Number of customers: 3", response.Body.AsString());
        }

        [Fact]
        public void mocking_dependencies_type()
        {
            // Arrange
            var browser = new Browser(cfg =>
                                          {
                                              cfg.Module<ModuleWithOneDependency>();
                                              cfg.Dependency<ICustomerRepository>(typeof(FakeCustomerRepository));
                                          });

            // Act
            var response = browser.Get(ROOT_URL);

            // Assert
            Assert.Equal("Number of customers: 3", response.Body.AsString());
        }

    }
}