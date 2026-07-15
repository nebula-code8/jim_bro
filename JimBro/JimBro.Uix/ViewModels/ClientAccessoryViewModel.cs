using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientAccessoryViewModel
{
    private readonly IClientAccessoryService _clientAccessoryService;
    private readonly IAccessoryService _accessoryService;
    private readonly Client _currentClient;
    
    public ObservableCollection<Accessory> AllAccessories { get; private set; } = new();
    public ObservableCollection<Accessory> ClientAccessories { get; private set; } = new();
    
    public Accessory? SelectedAccessory { get; set; }
    public Accessory? SelectedClientAccessory { get; set; }
    
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientAccessoryViewModel(IClientAccessoryService clientAccessoryService, IAccessoryService accessoryService, Client currentClient)
    {
        _clientAccessoryService = clientAccessoryService;
        _accessoryService = accessoryService;
        _currentClient = currentClient;
    }
    
    public void LoadAllAccessories()
    {
        try
        {
            var accessories = _accessoryService.GetAllAccessories();
            AllAccessories.Clear();
            foreach (var accessory in accessories)
                AllAccessories.Add(accessory);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška: {ex.Message}";
        }
    }
    
    public void LoadClientAccessories()
    {
        try
        {
            var accessories = _clientAccessoryService.GetAccessoriesForClient(_currentClient.Id);
            ClientAccessories.Clear();
            foreach (var accessory in accessories)
                ClientAccessories.Add(accessory);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška: {ex.Message}";
        }
    }
    
    public bool AddAccessoryToClient()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedAccessory == null)
                throw new Exception("Izaberite rekvizit!");
            
            foreach (var accessory in ClientAccessories)
            {
                if (accessory.Id == SelectedAccessory.Id)
                    throw new Exception("Već ste dodali ovaj rekvizit!");
            }
            
            _clientAccessoryService.AddAccessoryToClient(_currentClient.Id, SelectedAccessory.Id);
            LoadClientAccessories();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    
    public bool RemoveAccessoryFromClient()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedClientAccessory == null)
                throw new Exception("Izaberite rekvizit za brisanje!");
            
            _clientAccessoryService.RemoveAccessoryFromClient(_currentClient.Id, SelectedClientAccessory.Id);
            LoadClientAccessories();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}