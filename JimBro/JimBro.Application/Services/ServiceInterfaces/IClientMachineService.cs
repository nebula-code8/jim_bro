using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IClientMachineService
{
    void AddMachineToClient(long clientId, long machineId);
    void RemoveMachineFromClient(long clientId, long machineId);
    List<Machine> GetMachinesForClient(long clientId);
}