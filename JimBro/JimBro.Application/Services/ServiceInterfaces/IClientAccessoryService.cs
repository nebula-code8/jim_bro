using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IClientAccessoryService
{
    void AddAccessoryToClient(long clientId, long accessoryId);
    void RemoveAccessoryFromClient(long clientId, long accessoryId);
    List<Accessory> GetAccessoriesForClient(long clientId);
}