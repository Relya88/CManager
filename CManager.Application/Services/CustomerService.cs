using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CManager.Application.Services;

//logik för kundhantering i programmet som pratar med repositoryt 

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _customerRepo;

    public CustomerService(ICustomerRepo customerRepo)
    {
        _customerRepo = customerRepo;
    }

    //skapar ny kund och sparar i jsonfil. Även skapat adress kopplat til kund
    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        var customers = _customerRepo.GetAllCustomers();
        customers.Add(customerModel);
        return _customerRepo.SaveCustomers(customers);
    }

    //hämtar kunder från repositoryt och hanterar fel med haserror vid hämtning
    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;

        try
        {
            return _customerRepo.GetAllCustomers();
        }
        catch
        {
            hasError = true;
            return [];
        }
    }

    public CustomerModel? GetCustomerByEmail(string email)
    {
        var customers = _customerRepo.GetAllCustomers();
        return customers.FirstOrDefault(c => c.Email == email);
    }

    public bool DeleteCustomer(string email)
    {
        var customers = _customerRepo.GetAllCustomers();
        var customer = customers.FirstOrDefault(c => c.Email == email);

        if (customer == null)
            return false;

        customers.Remove(customer);
        return _customerRepo.SaveCustomers(customers);
    }
}
