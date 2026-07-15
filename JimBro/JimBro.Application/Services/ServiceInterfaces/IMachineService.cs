using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IMachineService
{
    List<Machine> GetAllMachines();
}