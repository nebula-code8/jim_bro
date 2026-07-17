using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class ClientAccessoryService : IClientAccessoryService
{
    private readonly IClientAccessoryRepository _clientAccessoryRepository;
    
    public ClientAccessoryService(IClientAccessoryRepository clientAccessoryRepository){
        _clientAccessoryRepository = clientAccessoryRepository;
    }
    
    public void AddAccessoryToClient(long clientId, long accessoryId) => _clientAccessoryRepository.AddAccessoryToClient(clientId,  accessoryId);
    public void RemoveAccessoryFromClient(long clientId, long accessoryId) => _clientAccessoryRepository.RemoveAccessoryFromClient(clientId, accessoryId);
    public List<Accessory> GetAccessoriesForClient(long clientId) => _clientAccessoryRepository.GetAccessoriesForClient(clientId);
}