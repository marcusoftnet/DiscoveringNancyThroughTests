using System;
using System.Linq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class Query_Headers_And_other_stuff
    {
        [Fact]
        public void sending_values_on_the_querystring()
        {
            // Arrange
            var browser = new Browser(with => with.Module<QueryAndOtherModule>());

            // Act
            var reponse = browser.Get("/query", with => with.Query("theValue", "42"));
            //var reponse = browser.Get("/query", with => with.Cookie("theValue", "42"));
            //var reponse = browser.Get("/query", with => with.Header("theValue", "42"));

            // Assert
            Assert.Equal(QueryAndOtherModule.ANSWER, reponse.Body.AsString());
        }

        [Fact]
        public void sending_values_via_cookies()
        {
            // Arrange
            var browser = new Browser(with => with.Module<QueryAndOtherModule>());

            // Act
            var reponse = browser.Get("/cookie", with => with.Cookie("theValue", "42"));

            // Assert
            Assert.Equal(QueryAndOtherModule.ANSWER, reponse.Body.AsString());
        }

        [Fact]
        public void sending_values_via_headers()
        {
            // Arrange
            var browser = new Browser(with => with.Module<QueryAndOtherModule>());

            // Act
            var reponse = browser.Get("/header", with => with.Header("theValue", "42"));

            // Assert
            Assert.Equal(QueryAndOtherModule.ANSWER, reponse.Body.AsString());
        }

        internal class QueryAndOtherModule : NancyModule
        {
            public const string ANSWER = "It's the answer";
            public const string QUESTION = "It's probably the question";

            public QueryAndOtherModule()
            {
                Get["/query"] = p => 
                            { return GetAnswer(Request.Query["theValue"]); };
                Get["/cookie"] = p => 
                            { return GetAnswer(Request.Cookies["theValue"]); };
                Get["/header"] = p =>
                            {
                                // Get the first header for this value
                                var value = Request.Headers["theValue"].FirstOrDefault();
                                return GetAnswer(value);
                            };
            }

            private static string GetAnswer(string parameter)
            {
                int value = int.Parse(parameter);
                return value == 42 ? ANSWER : QUESTION;
            }
        }
    }
}
