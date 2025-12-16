using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using NSubstitute;

namespace CManager.Tests;

//testar att CustomerService kan hitta en specifik kund baserat på eposten och testar bara logiken, inte infrastrukturen
//Följer AAA-metoden från lektionen och bekräftar att rätt kund returneras när eposten finns. 
public class CustomerServiceTests
{
    [Fact]
    public void GetCustomerByEmail_ShouldReturnCustomer_WhenEmailExists()
    {
        // arrange som skapar lista med testkunder
        var customers = new List<CustomerModel>
        {
            new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Testsson",
                Email = "testning@example.com",
                PhoneNumber = "123456789",
                Address = new AddressModel
                {
                    StreetAddress = "Streetnr 10",
                    PostalCode = "12345",
                    City = "City"
                }
            }
        };

        //  mockningen som jag fick ta hjälp av chatgpt då inte lyckades få till det utan att ändra repot och annat kod
        var mockRepo = Substitute.For<ICustomerRepo>();
        mockRepo.GetAllCustomers().Returns(customers);

        //service med den mockade repon
        var service = new CustomerService(mockRepo);

        // act som kallar på metoden som testas
        var result = service.GetCustomerByEmail("testning@example.com");

        //assert som kontrollerar resultatet inte är null och att emailen stämmer
        Assert.NotNull(result);
        Assert.Equal("testning@example.com", result.Email);
    }
}
