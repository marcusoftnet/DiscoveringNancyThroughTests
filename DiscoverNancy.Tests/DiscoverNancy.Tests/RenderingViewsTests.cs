using DiscoverNancy.Web;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class RenderingViewsTests
    {
        [Fact]
        public void should_be_able_to_view_things_in_the_rendred_view()
        {
            // Arrange
            var browser = new Browser(with => with.Module<ViewRenderingModule>());

            // Act
            var response = browser.Get("/renderView");

            // Assert
            response.Body[".message"]
                .ShouldExistOnce()
                .And.ShouldContain("Hello from the server!");
        }
    }
}
