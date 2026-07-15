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

public partial class ClientMachineWindow : Window
{
    private readonly ClientMachineViewModel _viewModel;
    private Machine? _selectedMachine;
    private Machine? _selectedClientMachine;
    public ClientMachineWindow(Client client)
    {
        InitializeComponent();
        _viewModel = new ClientMachineViewModel(new ClientMachineService(new ClientMachineDbRepository()),
            new MachineService(new MachineDbRepository()), client);
        DataContext = _viewModel;
        AllMachinesDataGrid.ItemsSource = _viewModel.AllMachines;
        ClientMachinesDataGrid.ItemsSource = _viewModel.ClientMachines;
        _viewModel.LoadAllMachines();
        _viewModel.LoadClientMachines();
        AllMachinesDataGrid.SelectionChanged += AllMachinesDataGrid_SelectionChanged;
        ClientMachinesDataGrid.SelectionChanged += ClientMachinesDataGrid_SelectionChanged;
    }
    
    private void AllMachinesDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedMachine = AllMachinesDataGrid.SelectedItem as Machine;
        _viewModel.SelectedMachine = _selectedMachine;
        if (_selectedMachine != null)
        {
            ClientMachinesDataGrid.SelectedItem = null;
            _viewModel.SelectedClientMachine = null;
            AddButton.IsEnabled = true;
            RemoveButton.IsEnabled = false;
        }
        else
        {
            AddButton.IsEnabled = false;
        }
    }
    
    private void ClientMachinesDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedClientMachine = ClientMachinesDataGrid.SelectedItem as Machine;
        _viewModel.SelectedClientMachine = _selectedClientMachine;
        if (_selectedClientMachine != null)
        {
            AllMachinesDataGrid.SelectedItem = null;
            _viewModel.SelectedMachine = null;
            RemoveButton.IsEnabled = true;
            AddButton.IsEnabled = false;
        }
        else
        {
            RemoveButton.IsEnabled = false;
        }
    }
    
    private void AddMachineButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.AddMachineToClient();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste dodali spravu!";
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
    
    private void RemoveMachineButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.RemoveMachineFromClient();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste obrisali spravu!";
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