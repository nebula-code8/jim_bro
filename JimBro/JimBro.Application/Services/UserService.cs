using System.Text.RegularExpressions;
using JimBro.Domain;

namespace JimBro.Services;

public abstract class UserService
{
    public void ValidateUser(User user)
    {
        ValidateRequiredFields(user);
        ValidateFormat(user);
        ValidateUserConstraints(user);
    }

    private static void ValidateRequiredFields(User user) {
        if (string.IsNullOrWhiteSpace(user.Name))
            throw new UserValidationException("Ime je obavezno polje!");

        if (string.IsNullOrWhiteSpace(user.Surname))
            throw new UserValidationException("Prezime je obavezno polje!");

        if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            throw new UserValidationException("Telefon je obavezno polje!");

        if (string.IsNullOrWhiteSpace(user.EmailAddress))
            throw new UserValidationException("Email je obavezno polje!");

        if (string.IsNullOrWhiteSpace(user.Password))
            throw new UserValidationException("Lozinka je obavezno polje!");
    }

    private static void ValidateFormat(User user) {
        if (!Regex.IsMatch(user.EmailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new UserValidationException("Nevažeči email!");

        if (!Regex.IsMatch(user.Name, @"^[a-zA-Z]+$"))
            throw new UserValidationException("Nevažeće ime!");

        if (!Regex.IsMatch(user.Surname, @"^[a-zA-Z]+$"))
            throw new UserValidationException("Nevažeće prezime!");
        
        string cleanedPhone = user.PhoneNumber.Replace("+", "").Replace("-", "").Replace(" ", "");
        if (!Regex.IsMatch(cleanedPhone, @"^\d{8,15}$"))
            throw new UserValidationException("Nevažeći broj telefona!");
    }

    private static void ValidateUserConstraints(User user) {
        if (user.Gender != Gender.Male && user.Gender != Gender.Female)
            throw new UserValidationException("Izaberite pol!");

        if (user.DateOfBirth == DateOnly.MinValue)
            throw new UserValidationException("Datum rođenja  je obavezno polje!");
        
        if (user.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            throw new UserValidationException("Datum rođenja  ne može  biti u budućnosti!");

        if (DateTime.Now.Year - user.DateOfBirth.Year < 6)
            throw new UserValidationException("Korisnik mora imati najmanje 6 godina!");

        if (user.Password.Length < 6)
            throw new UserValidationException("Lozinka mora najmanje imati 6 karaktera!");
    }
    
    public class UserValidationException : Exception
    {
        public UserValidationException(string message) : base(message) { }
    }
}