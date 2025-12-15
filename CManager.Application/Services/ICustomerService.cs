using CManager.Domain.Models;
using System.Collections.Generic;

namespace CManager.Application.Services;

public interface ICustomerService
{
    //Skapar ny kund
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);
    //hämtar alla kunder
    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);
    //baserat på email hämtas en specifik kund
    CustomerModel? GetCustomerByEmail(string email);
    //baserat på email tas kund bort
    bool DeleteCustomer(string email);
}

