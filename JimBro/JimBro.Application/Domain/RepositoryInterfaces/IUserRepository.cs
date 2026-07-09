namespace JimBro.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    (long Id, Role Role)? AuthenticateUser(string email, string password);
}