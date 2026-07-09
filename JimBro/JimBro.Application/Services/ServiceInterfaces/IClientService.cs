using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public  interface IClientService
{
    long CreateClient(Client client);
    int UpdateClient(Client client);
}