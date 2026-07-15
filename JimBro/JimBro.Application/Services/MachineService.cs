using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class MachineService : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    
    public MachineService(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    public List<Machine> GetAllMachines() => _machineRepository.GetAllMachines();
}