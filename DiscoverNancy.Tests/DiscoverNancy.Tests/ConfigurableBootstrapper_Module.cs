using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class ConfigurableBootstrapper_Module
    {
        [Fact]
        public void not_configuring_a_module_will_load_all_modules()
        {
            // Arrange
            var browser = new Browser(new DefaultNancyBootstrapper());

            // Act
            var response = browser.Get("/config");

            // Assert
            // The route /config was used and hence this should be returned
            Assert.Equal("Hello configured fellow", response.Body.AsString());
        }

        [Fact]
        public void basic_Module_typeparam()
        {
            // Arrange
            var browser = new Browser(cfg => cfg.Module<ConfigBootTestModule>());

            // Act
            var response = browser.Get("/config");

            // Assert
            Assert.Equal("Hello configured fellow", response.Body.AsString());
        }

        [Fact]            
        public void basic_Module_instance()
        {
            // Arrange
            var browser = new Browser(cfg => cfg.Module(new ConfigBootTestModule()));

            // Act
            var response = browser.Get("/config");

            // Assert
            Assert.Equal("Hello configured fellow", response.Body.AsString());
        }

        [Fact]
        public void basic_Module_instance_key()
        {
            // Arrange
            // Don't really get this one... 
            // You can apperantly register the Modules under keys... 
            var browser = new Browser(cfg => cfg.Module(new ConfigBootTestModule(), "MyModule"));

            // Act
            var response = browser.Get("/config");

            // Assert
            Assert.Equal("Hello configured fellow", response.Body.AsString());
        }

        [Fact]
        public void config_load_several_modules_types()
        {
            // Arrange
            var browser = new Browser(cfg =>
                cfg.Modules(typeof(ConfigBootTestModule), typeof(TestingNancyWithoutWebStuff.SimpleModule)));

            // Act
            // Since we have loaded the SimpleModule we can now safely access that route too
            var response = browser.Get("/simple");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
