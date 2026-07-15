using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class MyRequestsWindow : Window
{
    private readonly MyRequestsViewModel _viewModel;
    private readonly Client _currentClient;
    
    public MyRequestsWindow(Client client)
    {
        InitializeComponent();
        _currentClient = client;
        _viewModel = new MyRequestsViewModel(new TrainerService(new TrainerDbRepository()), new TrainerRequestService(new TrainerRequestDbRepository()), _currentClient);
        DataContext = _viewModel;
        TrainersDataGrid.ItemsSource = _viewModel.Trainers;
        _viewModel.LoadRequests();
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}