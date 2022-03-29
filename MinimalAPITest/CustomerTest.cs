using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.EndpointDefinitions;
using MinimalAPI.Models;
using MinimalAPI.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace MinimalAPITest;

public class CustomerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly CustomerRepository _repository;

    public CustomerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new CustomerRepository();
    }

    [Fact]
    public void AddCustomer()
    {
        CustomerEndpointDefinition customerEndpointDefinition = new CustomerEndpointDefinition();
        var result = customerEndpointDefinition.Create(new CustomerRepository(), new Customer(new Guid(), "Rick"));
        _testOutputHelper.WriteLine(result.ToString());
    }
    
    [Fact]
    public void GetCustomers()
    {
        CustomerEndpointDefinition customerEndpointDefinition = new CustomerEndpointDefinition();
        customerEndpointDefinition.Create(_repository, new Customer(new Guid(), "Rick"));
        customerEndpointDefinition.GetAll(_repository).Should().NotBeEmpty();
    }
}