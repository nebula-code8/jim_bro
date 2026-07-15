using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IEquipmentService
{
    List<Equipment> GetAllEquipments();
}