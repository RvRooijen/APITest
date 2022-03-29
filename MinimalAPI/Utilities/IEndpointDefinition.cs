namespace MinimalAPI.Utilities;

public interface IEndpointDefinition
{
    void DefineServices(IServiceCollection serviceCollection);
    void DefineEndpoints(WebApplication application);
}