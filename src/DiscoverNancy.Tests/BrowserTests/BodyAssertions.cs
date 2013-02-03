using Nancy;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Tests.BrowserTests
{
    public class BodyAssertions
    {
        // I shorten this up - it's the same for all tests
        private static BrowserResponse ResponseWithHtml
        {
            get
            {
                var browser = new Browser(with => with.Module<HtmlReturnerModule>());
                return browser.Get("/demoHtml/");
            }
        }

        [Fact]
        public void should_verify_precense_of_html_element_on_returned_reponse_via_class()
        {
            // Assert
            ResponseWithHtml.Body[".message"].ShouldExist();
        }

        [Fact]
        public void should_verify_precense_of_html_element_on_returned_reponse_via_id()
        {
            // Assert
            ResponseWithHtml.Body["#anId"].ShouldExistOnce();
        }

        [Fact]
        public void should_get_content_of_html_element_via_class()
        {
            // Assert
            ResponseWithHtml.Body[".message"].ShouldContain("A message");
        }

        [Fact]
        public void should_get_class_of_element_via_id()
        {
            // Assert
            ResponseWithHtml.Body["#anId"].ShouldBeOfClass("message2");
        }

        [Fact]
        public void should_be_able_to_check_several_things_in_a_row()
        {
            // Assert
            ResponseWithHtml
                .Body["#anSecondId"]
                .ShouldExistOnce()
                .And.ShouldContain("A second message with id");

        }
    }

    public class HtmlReturnerModule : NancyModule
    {
        public HtmlReturnerModule()
        {
            // Kids! This is not how you should do it!
            // It's better to return a view
            // in the demo case it sucks though since 
            // it will be yet another file for us to look in
            // hence the hardcoded HTML. 
            Get["/demoHtml"] = p => { return HTML; };
        }

        private const string HTML =
        @"
    <html>
        <head>
            <title>Marcus lazy demo page</title>
        </head>
        <body>
            <div class='message'>A message</div>
            <div id='anId' class='message2'>A message with id</div>
            <div id='anSecondId' class='message2' data-val='demo'>A second message with id</div>
        <body>
    </html>";
    }
}
