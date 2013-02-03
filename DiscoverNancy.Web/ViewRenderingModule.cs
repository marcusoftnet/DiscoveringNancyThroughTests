using Nancy;

namespace DiscoverNancy.Web
{
    public class ViewRenderingModule : NancyModule
    {
        public ViewRenderingModule()
        {
            Get["/renderView"] = p =>
                                     {
                                         var m = new ViewModel {Message = "Hello from the server"};
                                         return View["DemoView", m];
                                     };
        }
    }

    public class ViewModel
    {
        public string Message { get; set; }
    }
}