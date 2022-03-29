using MinimalAPI.Models;
namespace MinimalAPI.Repositories;

interface ICustomerRepository
{
    void Create(Customer? customer);
    public Customer? Get(Guid id);
    public List<Customer> GetAll();
    public void Update(Customer customer);
    public void Delete(Customer customer);
}

internal class CustomerRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, Customer> _customers = new();

    public void Create(Customer? customer)
    {
        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        
        _customers[customer.Id] = customer;
    }
    
    public Customer? Get(Guid id)
    {
        return _customers.GetValueOrDefault(id);
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