using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Uix.ViewModels;

public class LoginViewModel
{
    private readonly IUserRepository _userRepository;
    
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public string ErrorMessage { get; set; } = string.Empty;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    
    public (long UserId, Role Role)? AuthResult { get; private set; }
    
    public LoginViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public bool Authenticate()
    {
        ErrorMessage = string.Empty;
        AuthResult = null;
        
        var result = _userRepository.AuthenticateUser(Email, Password);
        
        if (result == null)
        {
            ErrorMessage = "Neispravan email ili lozinka.";
            return false;
        }
        
        AuthResult = result.Value;
        return true;
    }
}