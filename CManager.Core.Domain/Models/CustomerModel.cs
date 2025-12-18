namespace CManager.Core.Domain.Models;

//Står för en kund i systemet och klassen används i alla lager men utan logik

public class CustomerModel
{
    // guid för kundens unika id
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    //för bättre struktur skrevs addresssen som en separat modell 
    public AddressModel Address { get; set; } = null!;
}

