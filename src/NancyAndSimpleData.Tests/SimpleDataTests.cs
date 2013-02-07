using System.Collections.Generic;
using System.Linq;
using DiscoverNancy.Web;
using DiscoverNancy.Web.Models;
using DiscoverNancy.Web.Repositories;
using Nancy;
using Nancy.Testing;
using Nancy.ViewEngines.Razor;
using Simple.Data;
using Xunit;

namespace NancyAndSimpleData.Tests
{
    public class SimpleDataTests
    {
        private static dynamic _db;

        public SimpleDataTests()
        {
            // Con  figure the simple data InMemoryTest database
            // to have the properties we need to test it
            var adapter = new InMemoryAdapter();
            adapter.SetAutoIncrementKeyColumn("FairyTaleFigure", "ID");
            adapter.Join
                .Master("FairyTaleFigure", "ID")
                .Detail("FairyTaleFigure", "Hangarounds");

            Database.UseMockAdapter(adapter);
            _db = Database.Open();
        }

        [Fact]
        public void should_store_and_retrieve_simple_fairytale_figure()
        {
            // Arrange
            // SIMPLE.DATA STUFF
            // Configure the InMemoryAdapter, that we will test against
            var adapter = new InMemoryAdapter();
            adapter.SetAutoIncrementKeyColumn("FairyTaleFigure", "ID");

            // Tell Simple.Data to use the InMemoryAdapter we just created
            Database.UseMockAdapter(adapter);
            var db = Database.Open();

            // Store some data to retrieve
            var gollum = FairyTaleFigureTestDataFactory.Gollum;
            db.FairyTaleFigure.Insert(gollum);
            // END SIMPLE.DATA STUFF

            // Configure the NancyBrowser to use our module
            var browser = new Browser(with => with.Module<SimpleSimpleDataDemo>());

            // Act
            var response = browser.Get("/figure/" + gollum.Name);

            // Assert
            Assert.Equal("Gollum is Evil and has 0 hangarounds", response.Body.AsString());
        }

        [Fact]
        public void should_store_and_retrieve_complex_fairytale_figure()
        {
            // Arrange
            // Store some data to retrieve
            var snowWhite = FairyTaleFigureTestDataFactory.SnowWhite;
            _db.FairyTaleFigure.Insert(snowWhite);

            // Configure the NancyBrowser to use our module
            var browser = new Browser(with => with.Module<SimpleSimpleDataDemo>());

            // Act
            var response = browser.Get("/figure/" + snowWhite.Name);

            // Assert
            Assert.Equal("Snow White is Good and has 7 hangarounds", response.Body.AsString());
        }

        [Fact]
        public void full_stack_testing_with_module_in_web_project_with_razor_view()
        {
            // Arrange
            // Store some data to retrieve
            var snowWhite = FairyTaleFigureTestDataFactory.SnowWhite;
            _db.FairyTaleFigure.Insert(snowWhite);

            // Configure the NancyBrowser to use our module
            var browser = new Browser(with =>
                {
                    with.Module<FairyTaleFigureModule>();

                    // Let's configure Nancy.Testing to use our
                    // production repository. 
                    // Why not? See if I care. 
                    // Simple.Data let's us - and still not hit
                    // the database.
                    var instance = new FairyTaleFigureRepository();
                    with.Dependency<IFairyTaleFigureRepository>(instance);
                });

            // Act
            var path = string.Format("/figure/{0}/View/", snowWhite.Name);
            var response = browser.Get(path);

            // Assert
            response.Body[".fairyTaleFigure"]
                .ShouldExistOnce()
                .And.ShouldContain(snowWhite.Name);

            response.Body[".hangaround"].ShouldExist();
        }

        public class SimpleSimpleDataDemo : NancyModule
        {
            public SimpleSimpleDataDemo()
            {
                var db = Database.Open();
                Get["/figure/{Name}"] = p =>
                        {
                            string name = p.Name;
                            FairyTaleFigure f = db.FairyTaleFigure.FindByName(name);
                            return string.Format("{0} is {1} and has {2} hangarounds",
                                                    f.Name,
                                                    f.Evil ? "Evil" : "Good",
                                                    f.Hangarounds.Count());
                        };
            }
        }
    }

    public static class FairyTaleFigureTestDataFactory
    {
        public static FairyTaleFigure Gollum
        {
            get { return new FairyTaleFigure { Name = "Gollum", Evil = true }; }
        }
        public static FairyTaleFigure SnowWhite
        {
            get
            {
                return new FairyTaleFigure
                {
                    Evil = false,
                    Name = "Snow White",
                    Hangarounds = new[]
                                        {
                                            new FairyTaleFigure{Name = "Sleepy", Evil = false},
                                            new FairyTaleFigure{Name = "Doc", Evil = false},
                                            new FairyTaleFigure{Name = "Bashful", Evil = false},
                                            new FairyTaleFigure{Name = "Dopey", Evil = false},
                                            new FairyTaleFigure{Name = "Grumpy", Evil = true},
                                            new FairyTaleFigure{Name = "Happy", Evil = false},
                                            new FairyTaleFigure{Name = "Sneezy", Evil = false},
                                        }
                };
            }
        }
    }

}
