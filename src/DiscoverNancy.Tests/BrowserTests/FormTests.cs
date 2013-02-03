using Nancy;
using Nancy.ModelBinding;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class FormTests
    {
        [Fact]
        public void should_be_able_to_post_a_form()
        {
            // Arrange
            var browser = new Browser(with => with.Module<FormDemoModule>());

            // Act
            var response = browser.Post("/areYouOld", with =>
                    {
                        with.HttpRequest();
                        with.FormValue("Name", "Marcus");
                        with.FormValue("Age", "40");
                    });

            // Assert
            Assert.Equal("Wow, you're an old guy, Marcus!", response.Body.AsString());
        }

        internal class FormDemoModule : NancyModule
        {
            private const string INSULT_TO_HONORABLE_PEOPLE = "Wow, you're an old guy, {0}!";
            private const string NICE = "Still in your prime! Keep it up, {0}!";

            public FormDemoModule()
            {
                Post["/areYouOld"] = p =>
                                         {
                                             var person = this.Bind<Person>();
                                             return string.Format(person.Age > 39 ?
                                                                        INSULT_TO_HONORABLE_PEOPLE : 
                                                                        NICE, person.Name);
                                         };
            }
        }

        internal class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
