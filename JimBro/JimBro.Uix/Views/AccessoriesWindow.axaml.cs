using Avalonia.Controls;
using Avalonia.Interactivity;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class AccessoriesWindow : Window
{
    private readonly AccessoriesViewModel _viewModel;

    public AccessoriesWindow()
    {
        InitializeComponent();
        AccessoryDbRepository repository = new AccessoryDbRepository();
        AccessoryService service = new AccessoryService(repository);
        _viewModel = new AccessoriesViewModel(service);
        DataContext = _viewModel;
        _viewModel.LoadAccessories();
    }

    private async void AddAccessory_Click(object? sender, RoutedEventArgs e)
    {
        AccessoryFormWindow window = new AccessoryFormWindow(_viewModel);
        await window.ShowDialog(this);
    }

    private async void UpdateAccessory_Click(object? sender, RoutedEventArgs e)
    {
        ErrorMessageTextBlock.IsVisible = false;
        
        if (AccessoriesDataGrid.SelectedItem is not Accessory selectedAccessory)
        {
            ErrorMessageTextBlock.Text = "Izaberite rekvizit koji zelite da izmenite!";
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        
        AccessoryFormWindow window = new AccessoryFormWindow(_viewModel, selectedAccessory);
        await window.ShowDialog(this);
    }

    private void DeleteAccessory_Click(object? sender, RoutedEventArgs e)
    {
        ErrorMessageTextBlock.IsVisible = false;
        
        if (AccessoriesDataGrid.SelectedItem is not Accessory selectedAccessory)
        {
            ErrorMessageTextBlock.Text = "Izaberite rekvizit koji zelite da obrisete!";
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        
        bool deleted = _viewModel.DeleteAccessory(selectedAccessory.Id);
        
        if (!deleted)
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
    }
}