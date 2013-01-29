    using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class BrowserTests
    {
        [Fact]
        public void should_do_a_http_request()
        {
            // Arrange
            var browser = new Browser(with => with.Module<BrowserDemoModule>());

            // Act
            var response = browser.Get("/", with => with.HttpRequest());

            // Assert
            Assert.Equal("Hello HTTP", response.Body.AsString());
        }

        [Fact]
        public void should_do_a_https_request()
        {
            // Arrange
            var browser = new Browser(with => with.Module<BrowserDemoModule>());

            // Act
            var response = browser.Get("/", with => with.HttpsRequest());

            // Assert
            Assert.Equal("Hello HTTPS", response.Body.AsString());
        }

        internal class BrowserDemoModule : NancyModule
        {
            public BrowserDemoModule()
            {
                Get["/"] = _ =>
                               {
                                   return Request.Url.IsSecure ? "Hello HTTPS" : "Hello HTTP";
                               };
            }
        }
    }


}
