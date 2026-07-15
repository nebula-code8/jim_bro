using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class ClientsForTrainerWindow : Window
{
    private readonly ClientsForTrainerViewModel _viewModel;
    private readonly Trainer _currentTrainer;
    private readonly Client _selectedClient;
    
    public ClientsForTrainerWindow(Trainer trainer)
    {
        InitializeComponent();
        _currentTrainer = trainer;
        _viewModel =
            new ClientsForTrainerViewModel(new TrainerRequestService(new TrainerRequestDbRepository()), trainer);
        DataContext = _viewModel;
        ClientsDataGrid.ItemsSource = _viewModel.Clients;
        _viewModel.LoadClients();
    }

    private async void CreateWorkoutButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedClient == null)
        {
            ErrorMessageTextBlock.Text = "Izaberite klijenta!";
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        CreateWorkoutWindow window = new CreateWorkoutWindow(_currentTrainer, _viewModel.SelectedClient);
        await window.ShowDialog(this);
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}