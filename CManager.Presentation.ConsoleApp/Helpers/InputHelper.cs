using System;
using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers;

//olika varianter av valideringar
public enum ValidationType
{
    Required,
    Email,
}

//klass för att få in och kontrollera användarens input
public static class InputHelper
{
    // Ber användaren skriva in något och kontrollerar det
    public static string ValidateInput(string fieldName, ValidationType validationType)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            var input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is required.");
                Console.ReadKey();
                continue;
            }

            //validering beroende på typ
            var (isValid, errorMessage) = ValidateType(input, validationType);

            if (isValid)
                return input;

            Console.WriteLine(errorMessage);
            Console.ReadKey();
        }
    }

    //valideringslogik med regex som kontrolelrar att inputen är giltig med rätt tecken och format
    private static (bool isValid, string errorMessage) ValidateType(string input, ValidationType type)
    {
        return type switch
        {
            ValidationType.Required => (true, ""),
            ValidationType.Email =>
                Regex.IsMatch(input, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                    ? (true, "")
                    : (false, "Invalid email format. Example: name@example.com"),
            _ => (true, "")
        };
    }
}
