using System;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class ApplicationStartup_Tests
    {
        [Fact]
        public void shows_how_to_add_stuff_to_the_application_startup()
        {
            // Arrange
            // Ripped from the Nancy-testing tests
            // Let's play with the date of the application 
            // and kick ourself off 100 years in the future
            var date = new DateTime(2113, 01, 31);
            var bootstrapper = new ConfigurableBootstrapper(with =>
                {
                    with.Module<DateModule>();
                    with.ApplicationStartup((container, pipelines) =>
                    {
                        // Other options are:
                            // pipelines.AfterRequest
                            // pipelines.OnError

                        // But for now - let's hook in before each request
                        pipelines.BeforeRequest += ctx =>
                        {
                            ctx.Items.Add("date", date);
                            return null;
                        };
                    });
                });

            var browser = new Browser(bootstrapper);

            // Act
            var response = browser.Get("/dateInTheFuture");

            // Assert
            Assert.Equal("The date is: 2113-01-31", response.Body.AsString());
        }
    }

    public class DateModule : NancyModule
    {
        public DateModule()
        {
            Get["/dateInTheFuture"] = p =>
            {
                var dateString = Context.Items["date"].ToString();
                var dateFromContext = DateTime.Parse(dateString);

                return "The date is: " + dateFromContext.ToShortDateString();
            };
        }
    }
}
