using System.Linq;
using DiscoverNancy.Web;
using DiscoverNancy.Web.Repositories;
using Nancy.Testing;
using Xunit;

namespace DiscoverNancy.Specs.Steps.Wrappers
{
    public class FairyTaleModuleWrapper
    {
        private readonly Browser _browser;
        private BrowserResponse _latestResponse;

        public FairyTaleModuleWrapper()
        {
            _browser = new Browser(with =>
                {
                    with.Module<FairyTaleFigureModule>();
                    // Yeah - sure, let's use the production repository. 
                    // We have set up Simple.Data to do 
                    with.Dependency<IFairyTaleFigureRepository>(new FairyTaleFigureRepository());
                });
        }

        public void RegisterFairyTaleFigure(string name, string evilstring, int numberOfHangarounds = 0)
        {
            // Post a form via the browser
            // save the response 
            _latestResponse = _browser.Post("/figure", with =>
                    {
                        with.FormValue("Name", name);
                        AddEvilFormValue(with, evilstring);
                        AddHangaroundsFormValues(with, numberOfHangarounds);
                    });

            // We now expect a redirect according to the
            // Post Redirect Get (PRG) pattern
            // But we're really interested in the resulting 'page'
            // so we follow that link and get the result
            // view after the post back and save it in _latestResponse
            var expectedPath = "/figure/" + name;
            _latestResponse.ShouldHaveRedirectedTo(expectedPath);
            _latestResponse = _browser.Get(expectedPath);
        }

        public void VerifyFairyTaleName(string name)
        {
            _latestResponse.Body[".fairyTaleFigure"]
                .ShouldExistOnce()
                .And.ShouldContain(name);
        }

        public void VerifyFairyTaleEvilness(string evilstring)
        {
            _latestResponse.Body[".fairyTaleFigure"]
                .ShouldExistOnce()
                .And.ShouldContain(evilstring);
        }

        public void VerifyHangarounds(int numberOfHangarounds)
        {
            var s = _latestResponse.Body.AsString();
            var hangarounds = _latestResponse.Body[".hangaround"];
            Assert.Equal(numberOfHangarounds,
                         hangarounds.Count());
        }

        public void VerifyAllHangroundsEvilness(string evilstring)
        {
            _latestResponse.Body[".hangaround"]
                .Any(x => !x.InnerText.Contains(evilstring));
        }

        private void AddHangaroundsFormValues(BrowserContext with, int numberOfHangarounds)
        {
            if (numberOfHangarounds > 0)
            {
                for (var i = 0; i < numberOfHangarounds; i++)
                {
                    var number = i.ToString();
                    with.FormValue("Hangarounds.Name" + number, "Hangaround #" + number);
                }
            }
        }

        private void AddEvilFormValue(BrowserContext with, string evilstring)
        {
            var evil = evilstring.ToLower() == "good" ? false : true;
            with.FormValue("Evil", evil.ToString());
        }
    }   
}