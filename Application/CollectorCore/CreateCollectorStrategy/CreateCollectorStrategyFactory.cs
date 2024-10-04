namespace Application.CollectorCore.CreateCollectorStrategy;

public class CollectorCreationStrategyFactory
{
    public static IRoleCreationStrategy GetStrategy(string type)
    {
        return type switch
        {
            "E" => new CollectorManagerCreationStrategy(),
            "U" => new CollectorCreationStrategy(),
            _ => throw new ArgumentException("Rol no reconocido")
        };
    }
}
