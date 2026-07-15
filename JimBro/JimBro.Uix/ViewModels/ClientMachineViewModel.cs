using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientMachineViewModel
{
    private readonly IClientMachineService _clientMachineService;
    private readonly IMachineService _machineService;
    private readonly Client _currentClient;
    
    public ObservableCollection<Machine> AllMachines { get; private set; } = new();
    public ObservableCollection<Machine> ClientMachines { get; private set; } = new();
    
    public Machine? SelectedMachine { get; set; }
    public Machine? SelectedClientMachine { get; set; }
    
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientMachineViewModel(IClientMachineService clientMachineService, IMachineService machineService, Client currentClient)
    {
        _clientMachineService = clientMachineService;
        _machineService = machineService;
        _currentClient = currentClient;
    }
    
    public void LoadAllMachines()
    {
        try
        {
            var machines = _machineService.GetAllMachines();
            AllMachines.Clear();
            foreach (var machine in machines)
                AllMachines.Add(machine);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška: {ex.Message}";
        }
    }
    
    public void LoadClientMachines()
    {
        try
        {
            var machines = _clientMachineService.GetMachinesForClient(_currentClient.Id);
            ClientMachines.Clear();
            foreach (var machine in machines)
                ClientMachines.Add(machine);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška: {ex.Message}";
        }
    }
    
    public bool AddMachineToClient()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedMachine == null)
                throw new Exception("Izaberite spravu!");
            
            foreach (var machine in ClientMachines)
            {
                if (machine.Id == SelectedMachine.Id)
                    throw new Exception("Već ste dodali ovu spravu!");
            }
            
            _clientMachineService.AddMachineToClient(_currentClient.Id, SelectedMachine.Id);
            LoadClientMachines();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    
    public bool RemoveMachineFromClient()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedClientMachine == null)
                throw new Exception("Izaberite spravu za brisanje!");
            
            _clientMachineService.RemoveMachineFromClient(_currentClient.Id, SelectedClientMachine.Id);
            LoadClientMachines();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}