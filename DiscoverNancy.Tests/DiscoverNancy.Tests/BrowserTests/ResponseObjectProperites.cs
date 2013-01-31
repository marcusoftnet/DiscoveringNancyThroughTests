using System;
using System.Linq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class ResponseObjectProperites
    {
        internal static readonly DateTime tomorrow = DateTime.Now.AddDays(1);
    
        public BrowserResponse GetResponse(string url)
        {
            var browser = new Browser(with => with.Module<ResponseObjectModule>());
            return browser.Get(url);
        }

        [Fact]
        public void checking_out_the_headers()
        {
            // Act
            var response = GetResponse("/headers");

            // Assert
            Assert.Equal("42", response.Headers["marcusHeader"]);
        }

        [Fact]
        public void checking_out_the_cookies()
        {
            // Act
            var response = GetResponse("/cookies");

            // Assert
            var cookie = response.Cookies.Single(x => x.Name == "marcusCookie");
            Assert.Equal("42", cookie.Value);
            Assert.Equal(tomorrow.Date, cookie.Expires.Value.Date);
        }

        [Fact]
        public void body_in_different_formats()
        {
            // Arrange

            // Act 

            // Assert
        }
        
        public class ResponseObjectModule : NancyModule
        {
            public ResponseObjectModule()
            {
                Get["/headers"] = p => { return HeaderResponse(); };
                Get["/cookies"] = p => { return CookieResponse(); };
            }

            private static Response CookieResponse()
            {
                var response = new Response();
                response.AddCookie("marcusCookie", "42", tomorrow);
                return response;
            }

            private static Response HeaderResponse()
            {
                var response = new Response();
                response.Headers.Add("marcusHeader", "42");
                return response;
            }
        }
    }
}
