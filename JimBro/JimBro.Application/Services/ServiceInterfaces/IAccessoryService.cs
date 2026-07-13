using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IAccessoryService
{
    List<Accessory> GetAllAccessories();
    long CreateAccessory(Accessory accessory);
    int UpdateAccessory(Accessory accessory);
    int DeleteAccessory(long id);
}