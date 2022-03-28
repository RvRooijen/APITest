using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomerRepository>();

var app = builder.Build();

app.MapGet("/Customers", ([FromServices] CustomerRepository repository) => repository.GetAll());

app.MapGet("Customers/{id}", ([FromServices] CustomerRepository repository, Guid id) =>
{
    var customer = repository.Get(id);
    return customer is not null ? Results.Ok(customer) : Results.NotFound();
});

app.MapPost("Customers", ([FromServices] CustomerRepository repository, Customer customer) =>
{
    repository.Create(customer);
    return Results.Created($"/Customers/{customer.Id}", customer);
});

app.MapPut("Customers/{id}", ([FromServices] CustomerRepository repository, Guid id, Customer updatedCustomer) =>
{
    var customer = repository.Get(id);
    if (customer is null)
    {
        return Results.NotFound();
    }
    
    repository.Update(customer);
    return Results.Ok(customer);
});

app.MapDelete("Customers/{id}", ([FromServices] CustomerRepository repository, Guid id) =>
{
    var customer = repository.Get(id);
    if (customer is null)
    {
        return Results.NotFound();
    }
    
    repository.Delete(customer);
    return Results.NoContent();
});

app.Run();

record Customer(Guid Id, string Name);

class CustomerRepository
{
    private readonly Dictionary<Guid, Customer> _customers = new();

    public void Create(Customer customer)
    {
        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        
        _customers[customer.Id] = customer;
    }
    
    public Customer Get(Guid id)
    {
        return _customers[id];
    }
    
    public List<Customer> GetAll()
    {
        return _customers.Values.ToList();
    }

    public void Update(Customer customer)
    {
        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        
        _customers[customer.Id] = customer;
    }

    public void Delete(Customer customer)
    {
        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        
        _customers.Remove(customer.Id);
    }
}