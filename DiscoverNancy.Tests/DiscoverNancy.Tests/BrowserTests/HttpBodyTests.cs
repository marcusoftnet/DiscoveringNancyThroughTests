using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class HttpBodyTests
    {
        private const string _json = "[{ 'key1': 'value1' , 'key2': 'value2'},{ 'key1': 'value1' , 'key2': 'value2'}, { 'key1': 'value1' , 'key2': 'value2'}]";

        [Fact]
        public void should_be_able_to_post_a_raw_body()
        {
            // Given
            var browser = new Browser(with => with.Module<JsonModelBindingModule>());

            // When
            var result = browser.Post("/jsonlist", with =>
            {
                with.Body(_json);
                with.Header("content-type", "application/json");
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        
        [Fact]
        public void should_be_able_to_send_Json_directly()
        {
            // Given
            var browser = new Browser(with => with.Module<JsonModelBindingModule>());
            IEnumerable<MyModel> model = new []
                            {
                                new MyModel { key1 = "value 1", key2 = "value 2" },
                                new MyModel { key1 = "value 3", key2 = "value 4" },
                                new MyModel { key1 = "value 5", key2 = "value 6" },
                                new MyModel { key1 = "value 7", key2 = "value 8" }
                            };

            // When
            var result = browser.Post("/jsonlist", with => with.JsonBody(model));

            // Then
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        internal class JsonModelBindingModule : NancyModule
        {
            public JsonModelBindingModule()
            {
                Post["/jsonlist"] = _ =>
                {
                    var model = this.Bind<List<MyModel>>();

                    return (model.Count == 3) ?
                        HttpStatusCode.OK :
                        HttpStatusCode.InternalServerError;
                };
            }
        }

        internal class MyModel
        {
            public string key1 { get; set; }
            public string key2 { get; set; }
        }
    }
}
