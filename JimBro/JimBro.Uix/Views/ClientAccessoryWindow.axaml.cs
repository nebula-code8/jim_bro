using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class ClientAccessoryWindow : Window
{
    private readonly ClientAccessoryViewModel _viewModel;
    private Accessory? _selectedAccessory;
    private Accessory? _selectedClientAccessory;
    public ClientAccessoryWindow(Client client)
    {
        InitializeComponent();
        _viewModel = new ClientAccessoryViewModel(new ClientAccessoryService(new ClientAccessoryDbRepository()),
            new AccessoryService(new AccessoryDbRepository()), client);
        DataContext = _viewModel;
        AllAccessoriesDataGrid.ItemsSource = _viewModel.AllAccessories;
        ClientAccessoriesDataGrid.ItemsSource = _viewModel.ClientAccessories;
        _viewModel.LoadAllAccessories();
        _viewModel.LoadClientAccessories();
        AllAccessoriesDataGrid.SelectionChanged += AllAccessoriesDataGrid_SelectionChanged;
        ClientAccessoriesDataGrid.SelectionChanged += ClientAccessoriesDataGrid_SelectionChanged;
    }
    
    private void AllAccessoriesDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedAccessory = AllAccessoriesDataGrid.SelectedItem as Accessory;
        _viewModel.SelectedAccessory = _selectedAccessory;
        if (_selectedAccessory != null)
        {
            ClientAccessoriesDataGrid.SelectedItem = null;
            _viewModel.SelectedClientAccessory = null;
            AddButton.IsEnabled = true;
            RemoveButton.IsEnabled = false;
        }
        else
        {
            AddButton.IsEnabled = false;
        }
    }
    
    private void ClientAccessoriesDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedClientAccessory = ClientAccessoriesDataGrid.SelectedItem as Accessory;
        _viewModel.SelectedClientAccessory = _selectedClientAccessory;
        if (_selectedClientAccessory != null)
        {
            AllAccessoriesDataGrid.SelectedItem = null;
            _viewModel.SelectedAccessory = null;
            RemoveButton.IsEnabled = true;
            AddButton.IsEnabled = false;
        }
        else
        {
            RemoveButton.IsEnabled = false;
        }
    }
    
    private void AddAccessoryButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.AddAccessoryToClient();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste dodali rekvizit!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
            }
            else
            {
                ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
                ErrorMessageTextBlock.Foreground = Brushes.Red;
                ErrorMessageTextBlock.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.Foreground = Brushes.Red;
            ErrorMessageTextBlock.IsVisible = true;
        }
    }
    
    private void RemoveAccessoryButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.RemoveAccessoryFromClient();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste obrisali rekvizit!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
            }
            else
            {
                ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
                ErrorMessageTextBlock.Foreground = Brushes.Red;
                ErrorMessageTextBlock.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.Foreground = Brushes.Red;
            ErrorMessageTextBlock.IsVisible = true;
        }
    }
    
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}