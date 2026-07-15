using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class AccessoriesViewModel
{
    private readonly IAccessoryService _accessoryService;
    public ObservableCollection<Accessory> Accessories { get; } = new ObservableCollection<Accessory>();
    public string ErrorMessage { get; private set; } = string.Empty;
    public AccessoriesViewModel(IAccessoryService accessoryService)
    {
        _accessoryService = accessoryService;
    }
    
    public void LoadAccessories()
    {
        List<Accessory> accessories = _accessoryService.GetAllAccessories();
        Accessories.Clear();
        foreach (Accessory accessory in accessories)
        {
            Accessories.Add(accessory);
        }
    }
    public bool AddAccessory(string name, string description)
    {
        ErrorMessage = string.Empty;

        try
        {
            Accessory accessory = new Accessory(name.Trim(), description.Trim());
            _accessoryService.CreateAccessory(accessory);
            LoadAccessories();
            return true;
        }
        catch (ArgumentException exception)
        {
            ErrorMessage = exception.Message;
            return false;
        }
        catch (Exception)
        {
            ErrorMessage = "Došlo je do greške pri dodavanju rekvizita.";
            return false;
        }
    }
    
    public bool UpdateAccessory(long id, string name, string description)
    {
        ErrorMessage = string.Empty;

        try
        {
            Accessory accessory = new Accessory(id, name.Trim(), description.Trim());
            _accessoryService.UpdateAccessory(accessory);
            LoadAccessories();
            return true;
        }
        catch (ArgumentException exception)
        {
            ErrorMessage = exception.Message;
            return false;
        }
        catch (InvalidOperationException exception)
        {
            ErrorMessage = exception.Message;
            return false;
        }
        catch (Exception)
        {
            ErrorMessage = "Doslo je do greske pri izmeni rekvizita.";
            return false;
        }
    }
    
    public bool DeleteAccessory(long id)
    {
        ErrorMessage = string.Empty;
        try
        {
            _accessoryService.DeleteAccessory(id);
            LoadAccessories();
            return true;
        }
        catch (InvalidOperationException exception)
        {
            ErrorMessage = exception.Message;
            return false;
        }
        catch (Exception)
        {
            ErrorMessage = "Došlo je do greške pri brisanju rekvizita.";
            return false;
        }
    }

}