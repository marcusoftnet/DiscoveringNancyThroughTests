namespace ExternalDependencies
{
    public class SimpleDependency : ISimpleDependency
    {
        public string GetAName()
        {
            return "Albert";
        }
    }

    public interface ISimpleDependency
    {
        string GetAName();
    }
}
