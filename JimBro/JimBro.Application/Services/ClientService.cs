using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class ClientService : UserService, IClientService
{
    public readonly IClientRepository _clientRepository;
    
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public List<Client> GetAllClients()=> _clientRepository.GetAllClients();
    public void ValidateClient(Client client)
    {
        ValidateUser(client);
        
        if (client.Height <= 0 || client.Height > 300)
            throw new UserValidationException("Visina mora biti između 1 i 300 cm!");
            
        if (client.Weight <= 0 || client.Weight > 400)
            throw new UserValidationException("Težina mora biti između 1 i 400 kg!");
    }
    
    public long CreateClient(Client client)
    {
        ValidateClient(client);
        if (_clientRepository.GetByEmail(client.EmailAddress) != null) throw new Exception("Klijent već postoji!");
        long newId = _clientRepository.Insert(client);
        return newId;
    }
    
    public int UpdateClient(Client client)
    {
        ValidateClient(client);
        Client? existingClient = _clientRepository.GetById(client.Id);
        if (existingClient == null) throw new Exception("Klijent nije pronađen");
        if (existingClient.EmailAddress != client.EmailAddress) {
            Client? clientWithSameEmail = _clientRepository.GetByEmail(client.EmailAddress);
            if (clientWithSameEmail != null) throw new Exception("Email već postoji! Izaberite drugi email!");
        }
        int result = _clientRepository.Update(client);
        return result;
    }
}