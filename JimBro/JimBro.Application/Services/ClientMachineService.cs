using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class ClientMachineService : IClientMachineService
{
    private readonly IClientMachineRepository _clientMachineRepository;

    public ClientMachineService(IClientMachineRepository clientMachineRepository)
    {
        _clientMachineRepository = clientMachineRepository;
    }
    
    public void AddMachineToClient(long clientId, long machineId) => _clientMachineRepository.AddMachineToClient(clientId,  machineId);
    public void RemoveMachineFromClient(long clientId, long machineId) => _clientMachineRepository.RemoveMachineFromClient(clientId, machineId);
    public List<Machine> GetMachinesForClient(long clientId) => _clientMachineRepository.GetMachinesForClient(clientId);
}