using DiscoverNancy.Specs.Steps.Wrappers;
using TechTalk.SpecFlow;

namespace DiscoverNancy.Specs.Steps
{
    [Binding]
    public class FairyTaleFigureSteps
    {
        private FairyTaleModuleWrapper _fairyTaleModuleWrapper = new FairyTaleModuleWrapper();

        [When(@"I register a fairytale figure named: '(.*)' that is '(.*)'")]
        public void Register_Step(string name, string evilstring)
        {
            _fairyTaleModuleWrapper.RegisterFairyTaleFigure(name, evilstring);
        }

        [When(@"I register an '(.*)' fariytale figure with '(.*)' hangarounds")]
        public void RegisterHangarounds_Step(string evilstring, int numberOfHangarounds)
        {
            _fairyTaleModuleWrapper
                .RegisterFairyTaleFigure("WHATEVER", evilstring, numberOfHangarounds);
        }

        [Then(@"a new fairytale figure named: '(.*)' should have been registred")]
        public void VerifyName_Step(string name)
        {
            _fairyTaleModuleWrapper.VerifyFairyTaleName(name);
        }

        [Then(@"the fairytale figure should be '(.*)'")]
        public void ThenTheFairytaleFigureShouldBe(string evilstring)
        {
            _fairyTaleModuleWrapper.VerifyFairyTaleEvilness(evilstring);
        }

        [Then(@"the fairytale figure should have '(.*)' hangarounds")]
        public void VerifyHangarounds(int numberOfHangarounds)
        {
            _fairyTaleModuleWrapper.VerifyHangarounds(numberOfHangarounds);
        }

        [Then(@"every hangaround should be '(.*)'")]
        public void ThenEveryHangaroundShouldBe(string evilstring)
        {
            _fairyTaleModuleWrapper.VerifyAllHangroundsEvilness(evilstring);
        }
    }
}
