using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository repository)
    {
        _equipmentRepository = repository;
    }
    
    public List<Equipment> GetAllEquipments() => _equipmentRepository.GetAllEquipments();
}