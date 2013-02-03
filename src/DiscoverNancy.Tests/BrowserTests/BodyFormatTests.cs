using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class BodyFormatTests
    {
        private BrowserResponse GetResponse(string format)
        {
            var browser = new Browser(with => with.Module<BodyFormatModule>());
            return browser.Get("/bodyString/" + format);
        }

        [Fact]
        public void should_get_body_as_string()
        {
            // act
            var response = GetResponse("string");

            // Assert
            Assert.Equal(BodyFormatModule.STRING, response.Body.AsString());
        }
        
        [Fact]
        public void should_get_object_from_body_as_json()
        {
            // act
            var response = GetResponse("json");

            // Assert
            var kids = response.Body.DeserializeJson<IEnumerable<Kid>>();
            Assert.Equal(3, kids.Count());
            Assert.Equal(5, kids.Single(x => x.Name == "Albert").Age);
        }

        internal class Kid
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }


        public class BodyFormatModule : NancyModule
        {
            public const string XML = @"<?xml version='1.0' encoding='utf-8' ?>
                                        <kids>
                                            <kid name='Albert' age='5' />
                                            <kid name='Gustav' age='3' />
                                            <kid name='Arvid' age='3' />
                                        </kids>";
            public const string JSON = "[{ 'name': 'Gustav' , 'age': 3},{ 'name': 'Albert' , 'age': '5'}, { 'name': 'Arvid' , 'age': '3'}]";
            public const string STRING = "A string body";

            public BodyFormatModule()
            {
                Get["/bodyString/{format}"] = p => { return GetBody(p.Format); };
            }

            private string GetBody(string format)
            {
                switch (format)
                {
                    case "json":
                        return JSON;
                    case "xml":
                        return XML;
                    case "string":
                        return STRING;
                    default:
                        return "Bad format";
                }
            }
        }
    }
}
