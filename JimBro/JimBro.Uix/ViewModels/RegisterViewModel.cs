using JimBro.Domain;
using JimBro.Services;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class RegisterViewModel
{
    private readonly IClientService _clientService;

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public DateTime DateOfBirth {get; set;} = DateTime.Today.AddYears(-20);
    public string PhoneNumber { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public string HealthProblems { get; set; } = string.Empty;

    public string ErrorMessage { get; private set; } = string.Empty;
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public RegisterViewModel(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    public bool Register(Client client)
    {
        ErrorMessage = string.Empty;
        try
        {
            _clientService.CreateClient(client);
            return true;
        }
        catch (UserService.UserValidationException ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
        catch (Exception)
        {
            ErrorMessage = "Već postoji uneti email. Unesite drugi.";
            return false;
        }
    }
    
    public void CleanForm()
    {
        Name = string.Empty;
        Surname = string.Empty;
        Gender = Gender.Male;
        DateOfBirth = DateTime.Today.AddYears(-20);
        PhoneNumber = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Height = string.Empty;
        Weight = string.Empty;
        HealthProblems = string.Empty;
        ErrorMessage = string.Empty;
    }
}