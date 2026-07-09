namespace JimBro.Domain.RepositoryInterfaces;

public interface IClientRepository
{
    Client? GetById(long id);
    public Client? GetByEmail(string email);
    List<Client> GetAllClients();
    long Insert(Client client);
    int Update(Client client);
}