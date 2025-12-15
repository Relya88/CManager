using CManager.Domain.Models;
using System.Collections.Generic;

namespace CManager.Infrastructure.Repos;

//Interfacet som definierar vilka metoder som kundrepot måste ha med ansvar för att spara och hämta kunder från jsonfil
//listn hämtar kunder från jsonfil och returnerar dem som lista och boolen returnerar om sparnigenn lyckades
public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();

    bool SaveCustomers(List<CustomerModel> customers);
}
