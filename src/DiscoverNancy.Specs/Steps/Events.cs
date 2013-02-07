using Simple.Data;
using TechTalk.SpecFlow;

namespace DiscoverNancy.Specs.Steps
{
    [Binding]
    public class Events
    {
        [BeforeScenario]
        public void BeforeEachScenario()
        {
            var adapter = new InMemoryAdapter();
            adapter.SetAutoIncrementKeyColumn("FairyTaleFigure", "ID");
            adapter.Join
                .Master("FairyTaleFigure", "ID")
                .Detail("FairyTaleFigure", "Hangarounds");

            Database.UseMockAdapter(adapter);
            ScenarioContext.Current.Set(Database.Open(), ScenarioContextKeys.DB);
        }
    }

    public static class ScenarioContextKeys
    {
        public const string DB = "database_key";
    }
}
