using CManager.Core.Application.Services;
using CManager.Core.Domain.Models;
using CManager.Core.Infrastructure.Repos;
using NSubstitute;
using Xunit;

namespace CManager.Core.Tests.ServiceTests;

//Testar logiken i CustomerService. Repot mockas med NSubstitute
public class CustomerServiceTest

{
    [Fact]
    // Test 1: kund kan ej skapas (om kund med samma emai redan finns)
    public void CreateCustomer_ShouldReturnFalse_WhenEmailAlreadyExists()
    {
        // ARRANGE
        var repo = Substitute.For<ICustomerRepo>();

        repo.GetAll().Returns(new List<CustomerModel>
        {
            new CustomerModel { Email = "test@test.se" }
        });

        var service = new CustomerService(repo);

        // ACT
        var result = service.CreateCustomer(new CustomerModel
        {
            Email = "test@test.se"
        });

        // ASSERT
        Assert.False(result);
    }

    [Fact]
    // Test 2: en kund skapas korrekt (när emailen inte redan finns) 
 
    public void CreateCustomer_ShouldReturnTrue_WhenCustomerIsSaved()
    {
        // ARRANGE
        var repo = Substitute.For<ICustomerRepo>();

        repo.GetAll().Returns(new List<CustomerModel>());
        repo.Save(Arg.Any<List<CustomerModel>>()).Returns(true);

        var service = new CustomerService(repo);

        // ACT
        var result = service.CreateCustomer(new CustomerModel
        {
            Email = "new@test.se"
        });

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    // Test 3: Borttagning/delete av kund misslyckas om kunden inte hittas eller finns i systemet 
    public void DeleteCustomer_ShouldReturnFalse_WhenCustomerDoesNotExist()
    {
        // ARRANGE
        var repo = Substitute.For<ICustomerRepo>();
        repo.GetAll().Returns(new List<CustomerModel>());

        var service = new CustomerService(repo);

        // ACT
        var result = service.DeleteCustomer(Guid.NewGuid());

        // ASSERT
        Assert.False(result);
    }
}

