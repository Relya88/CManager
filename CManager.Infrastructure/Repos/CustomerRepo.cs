using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;

public class CustomerRepo : ICustomerRepo
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private readonly JsonSerializerOptions _jsonOptions;

    //konstruktor som sättter upp standardmapp och filnamn för kundlistan jsoninställningar för formatets utseende och flexibilitet
    public CustomerRepo(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };
    }

    //hömtar alla kunder från jsonfilen och om den ej finns retuneras tom lista
    public List<CustomerModel> GetAllCustomers()
    {
        if (!File.Exists(_filePath))
            return [];

        try
        {
            var json = File.ReadAllText(_filePath);
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
            return customers ?? [];
        }
        catch (Exception ex)
        {
            //konverterar jsontext till en lista objektet (customerModel)
            Console.WriteLine($"Error loading customers: {ex.Message}");
            throw;
        }
    }

    //hämtar kundlistan till jsonfil och sparar med undantag för om listan är null 
    public bool SaveCustomers(List<CustomerModel> customers)
    {
        if (customers == null)
            return false;

        try
        {
            var json = JsonSerializer.Serialize(customers, _jsonOptions);

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            //skriver fel när det ska sparas
            Console.WriteLine($"Error saving customers: {ex.Message}");
            return false;
        }
    }
}
