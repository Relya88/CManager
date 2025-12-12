namespace CManager.Domain.Models;

//klassen som beskriver en kund i systemet och innehåller all den information som behövs för att hantera kunden, 
//med allt från unikt id för varje kund som skapats med GUID till komplett adressobjekt tillhörande kund

public class CustomerModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public AddressModel Address { get; set; } = null!;
}
