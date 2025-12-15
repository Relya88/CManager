using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;
using System;
using System.Linq;


namespace CManager.Presentation.ConsoleApp.Controllers;

//klasse som hanterar allt som visas i menyn och använder servicen för att skapa, hämta och ta bort kunder.
public class MenuController
{
    private readonly ICustomerService _customerService; //servicen via dependency injection.

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    //håller igång programmet och visar huvudmenyn
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. View Specific Customer");
            Console.WriteLine("4. Delete Customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    ViewSpecificCustomer();
                    break;

                case "4":
                    DeleteCustomer();
                    break;

                case "0":
                    return;

                default:
                    OutputDialog("Invalid option! Press any key to continue...");
                    break;
            }
        }
    }

    // ny kund skapas från servicen 
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("Phone number", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Street Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("Postal Code", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer created successfully!");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }

        OutputDialog("Press any key to continue...");
    }

    // alla kunder visas 
    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong while loading customers.");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue...");
    }

    //detaljer om en specifik kund visas 
    private void ViewSpecificCustomer()
    {
        Console.Clear();
        Console.WriteLine("View Specific Customer");

        var email = InputHelper.ValidateInput("Enter email", ValidationType.Email);

        var customer = _customerService.GetCustomerByEmail(email);

        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
        }
        else
        {
            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.Address.StreetAddress}, {customer.Address.PostalCode} {customer.Address.City}");
        }

        OutputDialog("Press any key to continue...");
    }

    // Tar bort en kund baserat på email
    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete Customer");

        var email = InputHelper.ValidateInput("Enter email", ValidationType.Email);

        var result = _customerService.DeleteCustomer(email);

        if (result)
            Console.WriteLine("Customer removed successfully.");
        else
            Console.WriteLine("Customer not found.");

        OutputDialog("Press any key to continue...");
    }

    //mindre och enklare metod som väntar på att användaren trycker på en tangent
    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
