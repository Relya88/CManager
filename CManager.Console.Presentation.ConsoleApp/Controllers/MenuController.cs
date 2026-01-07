using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Application.Interfaces;
using CManager.Core.Domain.Models;

namespace CManager.Core.Presentation.ConsoleApp.Controllers;

//meny och användarflöde i konsolen

public class MenuController
{
    private readonly ICustomerService _customerService;

    // CustomerService injiceras via Dependency Injection
    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    //lagt till loop så att menyn visas om och om igen tills man själv avlutar programmet 
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager Core");
            Console.WriteLine("1. Create customer");
            Console.WriteLine("2. View customers");
            Console.WriteLine("3. Delete customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewCustomers();
                    break;

                case "3":
                    DeleteCustomer();
                    break;

                case "0":
                    //avslutar menyn och programmet
                    return;

                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Skapar kund med inskriven input iställer för använda helper. Tänker att det är enklare att validera med input direkt i controllern 
    //även valt enklare validering med if som kontrollera tom input. Man måste ange giltig namn och email
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create customer");

        Console.Write("First name: ");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine("Enter valid name. First name cannot be empty.");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            return;
        }

        Console.Write("Email: ");
        var email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Enter valid Email. Email cannot be empty.");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            return;
        }

        // Skapade ett nytt objekt (customerModel) med hjälp av chatgpt för varje kund alltid ska få ett unikt id (Guid.NweGuid)
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = "Test",
            Email = email,
            PhoneNumber = "000",
            Address = new AddressModel
            {
                Street = "-",
                PostalCode = "-",
                City = "-"
            }
        };

        //anropar servicelagret
        var result = _customerService.CreateCustomer(customer);

        Console.WriteLine(result
            ? "Customer created successfully"
            : "Customer already exists");

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }

    //det här ska visa alla mina kunder i systemet
    private void ViewCustomers()
    {
        Console.Clear();
        Console.WriteLine("All customers");

        var customers = _customerService.GetCustomers();

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} - {customer.Email}");
            }
        }

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }

    // Tar bort en kund baserat på mail men själva bortaggningen sker via kundes unika id i servicelagret
    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete customer");

        var customers = _customerService.GetCustomers();

        if (!customers.Any())
        {
            Console.WriteLine("No customers to delete.");
            Console.ReadKey();
            return;
        }

        // alla kunders mail
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.Email);
        }

        Console.WriteLine();
        Console.Write("Enter email of customer to delete: ");
        var email = Console.ReadLine();

        // letar efter kundesn mail
        var customerToDelete = customers
            .FirstOrDefault(c => c.Email == email);

        if (customerToDelete == null)
        {
            Console.WriteLine("Customer not found.");
            Console.ReadKey();
            return;
        }

        // förfrågan och bekräftelse på borttagning av kunden
        Console.WriteLine();
        Console.WriteLine($"Are you sure you want to delete customer '{customerToDelete.Email}'?");
        Console.Write("Type Y to confirm or N to cancel: ");
        var confirmation = Console.ReadLine();

        //måste bekräftas med yes annars avbryts det (tog hjälp av chatgpt för denna del)
        if (!string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Deletion cancelled.");
            Console.ReadKey();
            return;
        }

        //själva kundborttagningen
        var result = _customerService.DeleteCustomer(customerToDelete.Id);

        Console.WriteLine(result
            ? "Customer removed."
            : "Something went wrong.");

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }
}


