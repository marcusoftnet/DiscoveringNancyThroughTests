using System;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class RequestStartup_Tests
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
                with.Module<DateRequestModule>();
                with.RequestStartup((container, pipelines, context) => 
                        context.Items.Add("date", date));
            });

            var browser = new Browser(bootstrapper);

            // Act
            var response = browser.Get("/dateInTheFutureReq");

            // Assert
            Assert.Equal("The date is: 2113-01-31", response.Body.AsString());
        }
    }

    public class DateRequestModule : NancyModule
    {
        public DateRequestModule()
        {
            Get["/dateInTheFutureReq"] = p =>
            {
                var dateString = Context.Items["date"].ToString();
                var dateFromContext = DateTime.Parse(dateString);

                return "The date is: " + dateFromContext.ToShortDateString();
            };
        }
    }
}
