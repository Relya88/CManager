namespace CManager.Domain.Models;

//klassen som representerar en adress tillhörande kund
public class AddressModel
{
    public string StreetAddress { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
