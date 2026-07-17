namespace JimBro.Domain.RepositoryInterfaces;

public interface IClientMachineRepository
{
    void AddMachineToClient(long clientId, long machineId);
    void RemoveMachineFromClient(long clientId, long machineId);
    List<Machine> GetMachinesForClient(long clientId);
}