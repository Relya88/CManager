namespace CManager.Core.Domain.Models;

// Ren datamodell som innehåller endast information om adressen och ingen logik alls (som Emil visade).
public class AddressModel
{
    public string Street { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}

