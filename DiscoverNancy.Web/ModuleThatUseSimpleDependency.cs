using ExternalDependencies;
using Nancy;

namespace DiscoverNancy.Web
{
    public class ModuleThatUseSimpleDependency : NancyModule
    {
        public ModuleThatUseSimpleDependency()
        {
            // Don't ever do this - just proving the point that 
            // the assembly with SimpleDependency is not referenced by my tests
            var dependency = new SimpleDependency();
            Get["/simpleDep"] = p => { return dependency.GetAName(); };
        }
    }
}