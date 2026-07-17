namespace JimBro.Domain.RepositoryInterfaces;

public interface IClientAccessoryRepository
{
    void AddAccessoryToClient(long clientId, long accessoryId);
    void RemoveAccessoryFromClient(long clientId, long accessoryId);
    List<Accessory> GetAccessoriesForClient(long clientId);
}