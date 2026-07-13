using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class AccessoryService : IAccessoryService
{
    private readonly IAccessoryRepository _accessoryRepository;

    public AccessoryService(IAccessoryRepository accessoryRepository)
    {
        _accessoryRepository = accessoryRepository;
    }

    private static void ValidateAccessory(Accessory accessory)
    {
        if (string.IsNullOrWhiteSpace(accessory.Name))
        {
            throw new ArgumentException("Ime je obavezno polje!");
        }
        
        if (string.IsNullOrWhiteSpace(accessory.Description))
        {
            throw new ArgumentException("Opis je obavezno polje!");
        }

        if (accessory.Name.Length > 100)
        {
            throw new ArgumentException("Ime ne moze biti duze od 100 karaktera!");
        }
        
        if (accessory.Name.All(char.IsDigit))
        {
            throw new ArgumentException("Ime ne moze sastojati samo od cifara!");
        }
        
        if (accessory.Description.Length > 200)
        {
            throw new ArgumentException("Opis ne moze biti duzi od 200 karaktera!");
        }
    }

    public List<Accessory> GetAllAccessories()
    {
        return _accessoryRepository.GetAllAccessories();
    }
    
    public long CreateAccessory(Accessory accessory)
    {
        ValidateAccessory(accessory);
        return _accessoryRepository.Insert(accessory);
    }
    
    public int UpdateAccessory(Accessory accessory)
    {
        ValidateAccessory(accessory);
        int rowsAffected = _accessoryRepository.Update(accessory);

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException("Neuspesna operacija!");
        }
        
        return rowsAffected;
    }
    
    public int DeleteAccessory(long id)
    {
        int rowsAffected = _accessoryRepository.Delete(id);

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException("Neuspesna operacija!");
        }
        
        return rowsAffected;
    }
}

