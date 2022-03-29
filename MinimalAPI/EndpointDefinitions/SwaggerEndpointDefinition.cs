using Microsoft.OpenApi.Models;
using MinimalAPI.Utilities;

namespace MinimalAPI.EndpointDefinitions;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void DefineServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "MinimalAPI",
            Version = "v1"
        }));
    }

    public void DefineEndpoints(WebApplication application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPI"));
    }
}