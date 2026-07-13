namespace JimBro.Domain.RepositoryInterfaces;

public interface IAccessoryRepository
{
    List<Accessory> GetAllAccessories();
    long Insert(Accessory accessory);
    int Update(Accessory accessory);
    int Delete(long id);
}