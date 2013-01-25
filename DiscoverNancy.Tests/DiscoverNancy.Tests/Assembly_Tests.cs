using DiscoverNancy.Web;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class Assembly_Tests
    {
        [Fact]
        public void should_be_able_to_load_an_assembly_without_referencing_it()
        {
            // Arrange a browser for just our module
            // is using a type in an assembly that our tests
            // not is referencing
            var b = new Browser(with =>
                                    {
                                        with.Module<ModuleThatUseSimpleDependency>();
                                        with.Assembly("ExternalDependencies");
                                    });

            // Act 
            var r = b.Get("/simpleDep");

            // Assert
            Assert.Equal("Albert", r.Body.AsString());
        }
    }
}
