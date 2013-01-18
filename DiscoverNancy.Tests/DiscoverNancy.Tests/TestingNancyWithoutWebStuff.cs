using Nancy;    
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests
{
    public class TestingNancyWithoutWebStuff
    {
        [Fact]
        public void simplest_get_test()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new SimpleModule()));

            // Act
            var response = browser.Get("/simple");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }   

        [Fact]
        public void simplest_post_test()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new SimpleModule()));
            var username = "marcusoftnet";

            // Act
            var response = browser.Post("/simple/login/", with =>
            {
                with.HttpRequest();
                with.FormValue("Username", username);
                with.FormValue("Password", "really?YouThoughtIWouldRevealThat?");
            });

            // Assert
            response.ShouldHaveRedirectedTo("/simple/login?error=true&username=" + username);
        }


        /// <summary>
        /// Yeah - that's a complete web application class
        /// Inside my testing class library... 
        /// Nothing strange here
        /// </summary>
        public class SimpleModule : NancyModule
        {
            public SimpleModule() : base("/simple")
            {
                Get["/"] =  _ => { return HttpStatusCode.OK; };

                Get["/login"] = _ => { return View["Login"]; };
                Post["/login"] = parameters =>
                                {
                                    return HandleLogin(parameters);
                                };
            }

            private Response HandleLogin(dynamic parameters)
            {
                if (parameters.Password != "supersecret")
                    return Response.AsRedirect("/simple/login?error=true&username=marcusoftnet");

                return Response.AsRedirect("/simple/?username=marcusoftnet");

            }
        }
    }
}
