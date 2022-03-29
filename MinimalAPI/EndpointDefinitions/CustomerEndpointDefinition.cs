using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;
using MinimalAPI.Repositories;
using MinimalAPI.Utilities;

namespace MinimalAPI.EndpointDefinitions;

public class CustomerEndpointDefinition : IEndpointDefinition
{
    public void DefineServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICustomerRepository, CustomerRepository>();
    }

    public void DefineEndpoints(WebApplication application)
    {
        application.MapGet("/Customers", GetAll);
        application.MapGet("Customers/{id}", GetById);
        application.MapPost("Customers", Create);
        application.MapPut("Customers/{id}", Update);
        application.MapDelete("Customers/{id}", Delete);
    }

    internal List<Customer> GetAll(ICustomerRepository repository)
    {
        return repository.GetAll();
    }

    internal IResult GetById(ICustomerRepository repository, Guid id)
    {
        var customer = repository.Get(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal IResult Create(ICustomerRepository repository, Customer customer)
    {
        repository.Create(customer);
        return Results.Created($"/Customers/{customer.Id}", customer);
    }

    internal IResult Update(ICustomerRepository repository, Guid id, Customer updatedCustomer)
    {
        var customer = repository.Get(id);
        if (customer is null)
        {
            return Results.NotFound();
        }
    
        repository.Update(updatedCustomer);
        return Results.Ok(customer);
    }

    internal IResult Delete(ICustomerRepository repository, Guid id)
    {
        var customer = repository.Get(id);
        if (customer is null)
        {
            return Results.NotFound();
        }
    
        repository.Delete(customer);
        return Results.NoContent();
    }
}