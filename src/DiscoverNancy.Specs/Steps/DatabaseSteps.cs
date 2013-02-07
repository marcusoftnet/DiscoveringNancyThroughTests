using TechTalk.SpecFlow;

namespace DiscoverNancy.Specs.Steps
{
    [Binding]
    public class DatabaseSteps
    {
        [Given(@"there's no figures registered")]
        public void CleanFiguresInDB()
        {
            var db = ScenarioContext.Current.Get<dynamic>(ScenarioContextKeys.DB);
            db.FairyTaleFigure.DeleteAll(); 
        }
    }
}
